using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceOrderRepository : IGenericRepository<ServiceOrder>
    {
        Task<bool> GetActiveOrdersByVehicleIdAsync(int vehicleId);
        Task<IEnumerable<ServiceOrder>> GetActiveOrdersByClientIdAsync(int clientId);
        Task<IEnumerable<ServiceOrder>> GetServiceOrdersByClientIdentificationAsync(string identification);
        Task<bool> UpdateServiceOrderAuthorizationAsync(int serviceOrderId, bool isAuthorized, string? clientMessage);
    }
}