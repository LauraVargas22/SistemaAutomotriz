using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Application.Interfaces;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AuditoryRepository : GenericRepository<Auditory>, IAuditoryRepository
    {
        private readonly AutoTallerDbContext _context;

        public AuditoryRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }
    }
}