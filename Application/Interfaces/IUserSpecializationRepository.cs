using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserSpecializationRepository
    {
        Task<IEnumerable<UserSpecialization>> GetAllAsync();
        void Remove(UserSpecialization entity);
        void Update(UserSpecialization entity);
        Task<UserSpecialization?> GetByIdsAsync(int userId, int specializationId);
    }
}