using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiSGTA.Helpers;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ApiSGTA.Services;
using Microsoft.IdentityModel.Tokens;

namespace ApiSGTA.Services
{
    public class UserService : IUserService
    {
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,
            IPasswordHasher<User> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt.Value;
            _passwordHasher = passwordHasher;

        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var usuario = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Password = registerDto.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            usuario.Password = _passwordHasher.HashPassword(usuario, registerDto.Password!);

            var UsuarioExiste = _unitOfWork.UserRepository
                .Find(u => u.UserName.ToLower() == registerDto.Username.ToLower())
                .FirstOrDefault();

            if (UsuarioExiste == null)
            {
            var rolPredeterminado = _unitOfWork.RolRepository
                .Find(u => u.Description == UserAuthorization.rol_predeterminado.ToString())
                .FirstOrDefault();

            if (rolPredeterminado == null)
            {
                return $"El rol predeterminado '{UserAuthorization.rol_predeterminado}' no existe en la base de datos.";
            }


                try
                {
                    usuario.Rols.Add(rolPredeterminado);
                    _unitOfWork.UserRepository.Add(usuario);
                    
                    await _unitOfWork.SaveAsync();

                    return $"El usuario {registerDto.Username} hasido registrado exitosamente.";
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    return $"Error: {message}";
                }
            }
            else
            {
                return $"El usuario {registerDto.Username} ya existe.";
            }
        }

        public async Task<DataUserDto> GetTokenAsync(LoginDto model)
        {
            DataUserDto dataUserDto = new DataUserDto();
            var user = await _unitOfWork.UserRepository
                .GetByUsernameAsync(model.Username);

            if (user == null)
            {
                dataUserDto.EstaAutenticado = false;
                dataUserDto.Mensaje = $"No existe ningun usuario con el username {model.Username}.";
                return dataUserDto;
            }

            var resultado = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (resultado == PasswordVerificationResult.Success)
            {
                dataUserDto.EstaAutenticado = true;
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
                dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                dataUserDto.UserName = user.UserName;
                dataUserDto.Email = user.Email;
                dataUserDto.Rols = user.UserRols
                    .Select(ur => ur.Rol.Description)
                    .ToList();
                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive).FirstOrDefault();
                    dataUserDto.RefreshToken = activeRefreshToken.Token;
                    dataUserDto.RefreshTokenExpiration = activeRefreshToken.Expires;
                }
                else
                {
                    var refreshToken = CreateRefreshToken(user.Id);
                    dataUserDto.RefreshToken = refreshToken.Token;
                    dataUserDto.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    _unitOfWork.UserRepository.Update(user);
                    await _unitOfWork.SaveAsync();
                }
                return dataUserDto;
            }

            dataUserDto.EstaAutenticado = false;
            dataUserDto.Mensaje = $"Credenciales incorrectas para el usuario {model.Username}.";
            return dataUserDto;
        }

        public async Task<string> AddRoleAsync(AddRoleDto model)
        {
            var usuario = await _unitOfWork.UserRepository
                .GetByUsernameAsync(model.Username);

            if (usuario == null)
            {
                return $"No existe algún usuario registrado en la cuenta {model.Username}";
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

            if (resultado == PasswordVerificationResult.Success)
            {
                var rolExiste = _unitOfWork.RolRepository
                    .Find(r => r.Description.ToLower() == model.Role.ToLower())
                    .FirstOrDefault();

                if (rolExiste != null)
                {
                    var usuarioTieneRol = usuario.Rols
                        .Any(u => u.Id == rolExiste.Id);

                    if (usuarioTieneRol == false)
                    {
                        usuario.Rols.Add(rolExiste);
                        _unitOfWork.UserRepository.Update(usuario);
                        await _unitOfWork.SaveAsync();
                    }

                    return $"Rol {model.Role} agregado al usuario {model.Username} exitosamente.";
                }
                return $"Rol {model.Role} no existe.";
            }

            return $"Credenciales incorrectas para el usuario {model.Username}.";
        }
        private RefreshToken CreateRefreshToken(int userId)
        {
            var randomNumber = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    UserMemberId = userId,
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }

        public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
        {
            var datosUsuarioDto = new DataUserDto();

            var usuario = await _unitOfWork.UserRepository
                .GetByRefreshTokenAsync(refreshToken);

            if (usuario == null)
            {
                datosUsuarioDto.EstaAutenticado = false;
                datosUsuarioDto.Mensaje = $"El token no pertenece a ningún usuario.";
                return datosUsuarioDto;
            }

            var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

            if (!refreshTokenBd.IsActive)
            {
                datosUsuarioDto.EstaAutenticado = false;
                datosUsuarioDto.Mensaje = $"El token no está activo.";
                return datosUsuarioDto;
            }

            //Revocamos el Refresh Token actual y
            refreshTokenBd.Revoked = DateTime.UtcNow;
            //generamos un nuevo Refresh Token y lo guardamos en la Base de Datos
            var newRefreshToken = CreateRefreshToken(usuario.Id);
            usuario.RefreshTokens.Add(newRefreshToken);
            _unitOfWork.UserRepository.Update(usuario);
            await _unitOfWork.SaveAsync();
            //Generamos un nuevo Json Web Token 😊
            datosUsuarioDto.EstaAutenticado = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
            datosUsuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            datosUsuarioDto.Email = usuario.Email;
            datosUsuarioDto.UserName = usuario.UserName;
            datosUsuarioDto.Rols = usuario.UserRols
                                                .Select(ur => ur.Rol.Description)
                                                .ToList();
            datosUsuarioDto.RefreshToken = newRefreshToken.Token;
            datosUsuarioDto.RefreshTokenExpiration = newRefreshToken.Expires;
            return datosUsuarioDto;
        }
        
        private JwtSecurityToken CreateJwtToken(User usuario)
        {
            var roles = usuario.Rols;
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role.Description));
            }
            var claims = new[]
            {
                                    new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                    new Claim("uid", usuario.Id.ToString())
                            }
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
       