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

        public override async Task<User> GetByIdAsync(int id)
        {
            return await _context.User
                .FirstOrDefaultAsync(cc => cc.Id == id) ?? throw new KeyNotFoundException($"User with id {id} was not found");
        }
    }
}