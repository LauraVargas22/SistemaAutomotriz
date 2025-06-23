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
    public class TypeVehicleRepository : GenericRepository<TypeVehicle>, ITypeVehicleRepository
    {
        protected readonly AutoTallerDbContext _context;

        public TypeVehicleRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<TypeVehicle> GetByIdAsync(int id)
        {
            return await _context.TypeVehicles
                .FirstOrDefaultAsync(cc => cc.Id == id) ?? throw new KeyNotFoundException($"Type Vehicle with id {id} was not found");
        }
    }
}