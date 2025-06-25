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
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        protected readonly AutoTallerDbContext _context;

        public VehicleRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.Vehicle
                .FirstOrDefaultAsync(cc => cc.Id == id) ?? throw new KeyNotFoundException($"Vehicle with id {id} was not found");
        }    
        
        public override async Task<(int totalRegisters, IEnumerable<Vehicle> registers)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var consulta = _context.Vehicle as IQueryable<Vehicle>;

            if (!String.IsNullOrEmpty(search))
            {
                consulta = consulta.Where(v => 
                    EF.Functions.Like(v.Clients.Name.ToLower(), $"%{search.ToLower()}%") ||
                    EF.Functions.Like(v.VIN.ToLower(), $"%{search.ToLower()}%")
                );
            }

            var totalRegisters = await consulta.CountAsync();

            var registers = await consulta
                                    .Include(v => v.Clients)
                                    .Include(v => v.TypeVehicle)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (totalRegisters, registers);
        }
    }
}