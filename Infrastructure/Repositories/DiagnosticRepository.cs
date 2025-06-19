using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class DiagnosticRepository : GenericRepository<Diagnostic>, IDiagnosticRepository
    {
        public readonly AutoTallerDbContext _context;

        public DiagnosticRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }
    }
}