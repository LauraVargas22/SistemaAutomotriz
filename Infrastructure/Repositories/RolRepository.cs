using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RolRepository : GenericRepository<Rol>, IRolRepository
    {
        protected readonly AutoTallerDbContext _context;

        public RolRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }
    }
}