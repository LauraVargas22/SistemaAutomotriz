using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class DetailsDiagnosticRepository : GenericRepository<DetailsDiagnostic>, IDetailsDiagnosticRepository
    {
        public readonly AutoTallerDbContext _context;

        public DetailsDiagnosticRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

    }
}