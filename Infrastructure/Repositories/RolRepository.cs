using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        protected readonly AutoTallerDbContext _context;

        public RolRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Rol> GetByIdAsync(int id)
        {
            return await _context.Rol
                .FirstOrDefaultAsync(r => r.Id == id) ?? throw new KeyNotFoundException($"Rol with id {id} was not found");
        }
    }
}