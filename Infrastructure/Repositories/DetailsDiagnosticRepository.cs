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
    public class DetailsDiagnosticRepository : IDetailsDiagnosticRepository
    {
        public readonly AutoTallerDbContext _context;

        public DetailsDiagnosticRepository(AutoTallerDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<DetailsDiagnostic>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<DetailsDiagnostic?> GetByIdsAsync(int diagnosticId, int serviceOrderId)
        {
            return await _context.DetailsDiagnostics
                .FirstOrDefaultAsync(ur => ur.DiagnosticId == diagnosticId && ur.ServiceOrderId == serviceOrderId)
                ?? throw new KeyNotFoundException($"Details diagnostic with DiagnosticId {diagnosticId} and ServiceOrderId {serviceOrderId} was not found");
        }

        public void Remove(DetailsDiagnostic entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DetailsDiagnostic entity)
        {
            throw new NotImplementedException();
        }
    }
}