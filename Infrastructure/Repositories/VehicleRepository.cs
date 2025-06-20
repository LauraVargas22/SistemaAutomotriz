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
    }
}