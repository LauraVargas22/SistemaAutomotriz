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
    public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
    {
        public readonly AutoTallerDbContext _context;

        public OrderDetailsRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<OrderDetails> GetByIdAsync(int id)
        {
            return await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.Id == id) ?? throw new KeyNotFoundException($"Order Details with id {id} was not found");
        }

        public async Task<IEnumerable<OrderDetails>> GetByServiceOrderIdAsync(int serviceOrderId)
{
            return await _context.OrderDetails
                .Where(od => od.ServiceOrderId == serviceOrderId)
                .ToListAsync();
}
    }
}