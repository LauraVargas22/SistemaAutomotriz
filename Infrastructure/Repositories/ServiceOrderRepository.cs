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

        public override async Task<(int totalRegisters, IEnumerable<ServiceOrder> registers)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var consulta = _context.ServiceOrder as IQueryable<ServiceOrder>;

            if (!String.IsNullOrEmpty(search))
            {
                consulta = consulta.Where(so => 
                    EF.Functions.Like(so.EntryDate.ToString(), $"%{search.ToLower()}%") ||
                    EF.Functions.Like(so.Vehicle.Clients.Name.ToLower(), $"%{search.ToLower()}%") ||
                    EF.Functions.Like(so.State.Name.ToLower(), $"%{search.ToLower()}%") ||
                    EF.Functions.Like(so.Users.Name.ToLower(), $"%{search.ToLower()}%")
                );
            }

            var totalRegisters = await consulta.CountAsync();

            var registers = await consulta
                                    .Include(so => so.Vehicle)
                                        .ThenInclude(v => v.Clients)
                                    .Include(so => so.State)
                                    .Include(so => so.TypeService)
                                    .Include(so => so.Users)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (totalRegisters, registers);
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