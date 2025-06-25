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
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public readonly AutoTallerDbContext _context;

        public InvoiceRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoice
                .FirstOrDefaultAsync(i => i.Id == id) ?? throw new KeyNotFoundException($"Invoice with id {id} was not found");
        }

        public override async Task<(int totalRegisters, IEnumerable<Invoice> registers)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var consulta = _context.Invoice as IQueryable<Invoice>;

            if (!String.IsNullOrEmpty(search))
            {
                consulta = consulta.Where(i =>
                EF.Functions.Like(i.Date.ToString(), $"%{search.ToLower()}%") ||
                EF.Functions.Like(i.Code.ToLower(), $"%{search.ToLower()}%") ||
                EF.Functions.Like(i.ServiceOrders.Vehicle.Clients.Name.ToLower(), $"%{search.ToLower()}%"));
            }

            var totalRegisters = await consulta.CountAsync();

            var registers = await consulta
                            .Include(i => i.ServiceOrders)
                                .ThenInclude(so => so.Vehicle)
                                .ThenInclude(v => v.Clients)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

            return (totalRegisters, registers);
        }
    }
}