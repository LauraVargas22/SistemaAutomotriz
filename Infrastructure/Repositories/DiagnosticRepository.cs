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
    public class DiagnosticRepository : GenericRepository<Diagnostic>, IDiagnosticRepository
    {
        public readonly AutoTallerDbContext _context;

        public DiagnosticRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Diagnostic> GetByIdAsync(int id)
        {
            return await _context.Diagnostic
                .FirstOrDefaultAsync(d => d.Id == id) ?? throw new KeyNotFoundException($"Diagnostic with id {id} was not found");
        }
    }
}