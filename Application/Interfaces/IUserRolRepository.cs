using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRolRepository
    {
        Task<IEnumerable<UserRol>> GetAllAsync();
        void Remove(UserRol entity);
        void Update(UserRol entity);
        Task<UserRol?> GetByIdsAsync(int userId, int rolId);
    }
}