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
        private readonly IHttpContextAccessor _httpContextAccesor;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt,
            IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt.Value;
            _passwordHasher = passwordHasher;
            _httpContextAccesor = httpContextAccessor;
        }

        public string GetCurrentUser()
        {
            var user = _httpContextAccesor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                return "Anonymous";

            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "UnKnown";
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var usuario = new User
            {
                Name = registerDto.Name,
                LastName = registerDto.LastName,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
            };

            // 🔐 Hashear contraseña y mostrar en consola
            var hashedPassword = _passwordHasher.HashPassword(usuario, registerDto.Password!);
            Console.WriteLine($"🔐 Hashed: {hashedPassword}");
            usuario.Password = hashedPassword;

            var UsuarioExiste = _unitOfWork.UserRepository
                .Find(u => u.UserName.ToLower() == registerDto.Username.ToLower())
                .FirstOrDefault();

            if (UsuarioExiste == null)
            {
                try
                {
                    _unitOfWork.UserRepository.Add(usuario);
                    await _unitOfWork.SaveAsync();

                    return $"El usuario {registerDto.Username} ha sido registrado exitosamente.";
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
                dataUserDto.Id          = user.Id;
                dataUserDto.Name        = user.Name;
                dataUserDto.LastName    = user.LastName;
                dataUserDto.IsActive    = user.IsActive;
                dataUserDto.CreatedAt   = user.CreatedAt.ToDateTime(TimeOnly.MinValue);
                dataUserDto.UpdatedAt   = user.UpdatedAt.ToDateTime(TimeOnly.MinValue);
                dataUserDto.EstaAutenticado = true;
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
                dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                dataUserDto.UserName = user.UserName;
                dataUserDto.Email = user.Email;
                dataUserDto.Rols = user.UserRoles
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
                    dataUserDto.RefreshToken         = refreshToken.Token;
                    dataUserDto.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                }
                return dataUserDto;
            }

            dataUserDto.EstaAutenticado = false;
            dataUserDto.Mensaje = $"Credenciales incorrectas para el usuario {model.Username}.";
            return dataUserDto;
        }

        public async Task<string> AddRoleAsync(AddRoleDto model)
        {
            var usuario = await _unitOfWork.UserRepository.GetByUsernameAsync(model.Username);

            if (usuario == null)
            {
                return $"No existe algún usuario registrado con el username '{model.Username}'.";
            }

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);
            if (resultado != PasswordVerificationResult.Success)

            {
                return $"Credenciales incorrectas para el usuario {model.Username}.";
            }

            var rolExiste = _unitOfWork.RolRepository
                .Find(r => r.Description.ToLower() == model.Role.ToLower())
                .FirstOrDefault();

            if (rolExiste == null)
            {
                return $"El rol '{model.Role}' no existe.";
            }

            // Verifica si ya tiene el rol asignado
            var yaTieneRol = usuario.UserRoles.Any(ur => ur.RolId == rolExiste.Id);

            if (yaTieneRol)
            {
                return $"El usuario {model.Username} ya tiene el rol '{model.Role}'.";
            }

            // Asignar el rol mediante la tabla intermedia
            var userRol = new UserRol
            {
                UserId = usuario.Id,
                RolId = rolExiste.Id
            };

            usuario.UserRoles ??= new List<UserRol>();
            usuario.UserRoles.Add(userRol);

            _unitOfWork.UserRepository.Update(usuario);
            await _unitOfWork.SaveAsync();

            return $"Rol '{model.Role}' agregado al usuario '{model.Username}' exitosamente.";
        }

        private RefreshToken CreateRefreshToken(int userId)
        {
            var randomNumber = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    UserId = userId,
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddMinutes(1),
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
            datosUsuarioDto.Rols = usuario.UserRoles
                                                .Select(ur => ur.Rol.Description)
                                                .ToList();
            datosUsuarioDto.RefreshToken = newRefreshToken.Token;
            datosUsuarioDto.RefreshTokenExpiration = newRefreshToken.Expires;
            return datosUsuarioDto;
        }
        
        private JwtSecurityToken CreateJwtToken(User usuario)
        {
            // var roles = usuario.Rols;
            var roleClaims = new List<Claim>();
            foreach (var userRole in usuario.UserRoles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, userRole.Rol.Description));
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
       