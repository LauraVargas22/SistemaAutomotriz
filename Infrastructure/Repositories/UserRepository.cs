using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        protected readonly AutoTallerDbContext _context;

        public UserRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.User
                            .Include(u=>u.UserRols)
                            .ThenInclude(ur=>ur.Rol)
                            .Include(u => u.RefreshTokens)
                            .FirstOrDefaultAsync(u=>u.UserName.ToLower()==username.ToLower());
        }
        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.User
                        .Include(u=>u.UserRols)
                            .ThenInclude(ur=>ur.Rol)
                        .Include(u => u.RefreshTokens)
                        .FirstOrDefaultAsync(u=>u.RefreshTokens.Any(t=>t.Token==refreshToken));
        }
    }
}