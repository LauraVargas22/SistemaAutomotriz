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
        
        public override async Task<(int totalRegisters, IEnumerable<SparePart> registers)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var consulta = _context.SparePart as IQueryable<SparePart>;

            if (!String.IsNullOrEmpty(search))
            {
                consulta = consulta.Where(sp => 
                    EF.Functions.Like(sp.Description.ToLower(), $"%{search.ToLower()}%") ||
                    EF.Functions.Like(sp.Category.ToLower(), $"%{search.ToLower()}%") ||
                    EF.Functions.Like(sp.MiniStock.ToString(), $"%{search.ToLower()}%")
                );
            }

            var totalRegisters = await consulta.CountAsync();

            var registers = await consulta
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (totalRegisters, registers);
        }
    }
}