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
    }
}