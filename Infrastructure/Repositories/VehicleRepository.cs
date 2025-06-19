using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Application.Interfaces;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        protected readonly AutoTallerDbContext _context;

        public VehicleRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }        
    }
}