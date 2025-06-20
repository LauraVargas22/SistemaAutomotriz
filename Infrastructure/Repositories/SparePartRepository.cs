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
    public class SparePartRepository : GenericRepository<SparePart>, ISparePartRepository
    {
        protected readonly AutoTallerDbContext _context;

        public SparePartRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }
        
        public override async Task<SparePart> GetByIdAsync(int id)
        {
            return await _context.SparePart
                .FirstOrDefaultAsync(sp => sp.Id == id) ?? throw new KeyNotFoundException($"Spare Part with id {id} was not found");
        }
    }
}