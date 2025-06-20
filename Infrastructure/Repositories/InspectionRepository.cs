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
    public class InspectionRepository : GenericRepository<Inspection>, IInspectionRepository
    {
        public readonly AutoTallerDbContext _context;

        public InspectionRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Inspection> GetByIdAsync(int id)
        {
            return await _context.Inspection
                .FirstOrDefaultAsync(i => i.Id == id) ?? throw new KeyNotFoundException($"Inspection with id {id} was not found");
        }
    }
}