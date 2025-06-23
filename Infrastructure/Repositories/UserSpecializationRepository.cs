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
    public class UserSpecializationRepository : IUserSpecializationRepository
    {
        protected readonly AutoTallerDbContext _context;

        public UserSpecializationRepository(AutoTallerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserSpecialization>> GetAllAsync()
        {
            return await _context.Set<UserSpecialization>().ToListAsync();
        }

        public async Task<UserSpecialization?> GetByIdsAsync(int specializationId, int userId)
        {
            return await _context.UserSpecialization
                .FirstOrDefaultAsync(ur => ur.SpecializationId == specializationId && ur.UserId == userId)
                ?? throw new KeyNotFoundException($"User Specialization with SpecializationId {specializationId} and UserId {userId} was not found");
        }

        public void Remove(UserSpecialization entity)
        {
            _context.Set<UserSpecialization>().Remove(entity);
        }

        public void Update(UserSpecialization entity)
        {
            _context.Set<UserSpecialization>()
                .Update(entity);
        }
    }
}