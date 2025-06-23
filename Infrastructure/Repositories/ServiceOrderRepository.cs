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
    public class ServiceOrderRepository : GenericRepository<ServiceOrder>, IServiceOrderRepository
    {
        protected readonly AutoTallerDbContext _context;

        public ServiceOrderRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ServiceOrder> GetByIdAsync(int id)
        {
            return await _context.ServiceOrder
                .FirstOrDefaultAsync(so => so.Id == id) ?? throw new KeyNotFoundException($"Service Order with id {id} was not found");
        }

        public async Task<bool> GetActiveOrdersByVehicleIdAsync(int vehicleId)
        {
            return await _context.ServiceOrder
                    .AnyAsync(so => so.VehiclesId == vehicleId && (so.StateId == 2 || so.StateId == 3));
        }

        public async Task<IEnumerable<ServiceOrder>> GetActiveOrdersByClientIdAsync(int clientId)
        {
            return await _context.ServiceOrder
                .Include(so => so.Vehicle)
                .Where(so => so.Vehicle.ClientId == clientId && (so.StateId == 2 || so.StateId == 3))
                .ToListAsync();
        }       
    }
}