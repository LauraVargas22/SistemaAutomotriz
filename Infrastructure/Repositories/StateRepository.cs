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
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        protected readonly AutoTallerDbContext _context;

        public StateRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<State> GetByIdAsync(int id)
        {
            return await _context.State
                .FirstOrDefaultAsync(cc => cc.Id == id) ?? throw new KeyNotFoundException($"State with id {id} was not found");
        }
    }
}