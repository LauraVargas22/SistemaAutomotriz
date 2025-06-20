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
    public class DetailInspectionRepository : GenericRepository<DetailInspection>, IDetailInspectionRepository
    {
        public readonly AutoTallerDbContext _context;

        public DetailInspectionRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<DetailInspection> GetByIdAsync(int id)
        {
            return await _context.DetaillInspection
                .FirstOrDefaultAsync(di => di.Id == id) ?? throw new KeyNotFoundException($"Detail Inspection with id {id} was not found");
        }
    }
}