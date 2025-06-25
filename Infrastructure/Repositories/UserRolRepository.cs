using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRolRepository : IUserRolRepository
    {
        private readonly AutoTallerDbContext _context;

        public UserRolRepository(AutoTallerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRol>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRol?> GetByIdsAsync(int userId, int rolId)
        {
            return await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RolId == rolId)
                ?? throw new KeyNotFoundException($"UserRol with UserId {userId} and RolId {rolId} was not found");
        }

        public void Remove(UserRol entity)
        {
            _context.UserRoles.Remove(entity);
        }

        public void Update(UserRol entity)
        {
            _context.UserRoles.Update(entity);
        }
    }
}
