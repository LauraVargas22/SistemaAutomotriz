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
    public class TypeServiceRepository : GenericRepository<TypeService>, ITypeServiceRepository
    {
        protected readonly AutoTallerDbContext _context;

        public TypeServiceRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<TypeService> GetByIdAsync(int id)
        {
            return await _context.TypeService
                .FirstOrDefaultAsync(cc => cc.Id == id) ?? throw new KeyNotFoundException($"Type Service with id {id} was not found");
        }
    }
}