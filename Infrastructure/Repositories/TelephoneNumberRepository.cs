using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TelephoneNumbersRepository : GenericRepository<TelephoneNumbers>, ITelephoneNumbersRepository
    {
        private readonly AutoTallerDbContext _context;

        public TelephoneNumbersRepository(AutoTallerDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<TelephoneNumbers> GetByIdAsync(int id)
        {
            var result = await _context.Set<TelephoneNumbers>()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (result == null)
                throw new KeyNotFoundException($"Telephone number with id {id} not found");

            return result;
        }
    }
}
