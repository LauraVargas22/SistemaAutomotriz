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
    public class DetailsDiagnosticRepository : GenericRepository<DetailsDiagnostic>, IDetailsDiagnosticRepository
    {
        public readonly AutoTallerDbContext _context;

        public DetailsDiagnosticRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DetailsDiagnostic> GetByIdAsync(int diagnosticId, int serviceOrderId)
        {
            return await _context.DetailsDiagnostics
                .FirstOrDefaultAsync(ur => ur.DiagnosticId == diagnosticId && ur.ServiceOrderId == serviceOrderId)
                ?? throw new KeyNotFoundException($"Details diagnostic with DiagnosticId {diagnosticId} and ServiceOrderId {serviceOrderId} was not found");
        }
    }
}