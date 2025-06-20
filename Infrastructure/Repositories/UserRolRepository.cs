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
    public class UserRolRepository : GenericRepository<UserRol>, IUserRolRepository
    {
        protected readonly AutoTallerDbContext _context;

        public UserRolRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserRol> GetByIdAsync(int userId, int rolId)
        {
            return await _context.UserRole
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RolId == rolId)
                ?? throw new KeyNotFoundException($"UserRol with UserId {userId} and RolId {rolId} was not found");
        }
    }
}