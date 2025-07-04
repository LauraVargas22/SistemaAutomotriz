using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;


namespace Application.Services
{
    public class ClientServiceOrderService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ClientServiceOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<ClientServiceOrderDto>> GetServiceOrdersByClientIdentificationAsync(string identification)
        {
            var serviceOrders = await _unitOfWork.ServiceOrderRepository.GetServiceOrdersByClientIdentificationAsync(identification);
            
            var clientServiceOrderDtos = new List<ClientServiceOrderDto>();
            
            foreach (var order in serviceOrders)
            {
                var dto = new ClientServiceOrderDto
                {
                    Id = order.Id,
                    VehiclesId = order.VehiclesId,
                    TypeServiceId = order.TypeServiceId,
                    StateId = order.StateId,
                    EntryDate = order.EntryDate ?? DateOnly.FromDateTime(DateTime.Now),
                    ExitDate = order.ExitDate ?? DateOnly.FromDateTime(DateTime.Now),
                    IsAuthorized = order.IsAuthorized,
                    ClientMessage = order.ClientMessage,
                    UserId = order.UserId,
                    VehicleBrand = order.Vehicle?.Brand,
                    VehicleModel = order.Vehicle?.Model,
                    VehicleVIN = order.Vehicle?.VIN,
                    TypeServiceName = order.TypeService?.Name,
                    TypeServicePrice = order.TypeService?.Price ?? 0,
                    TypeServiceDuration = order.TypeService?.Duration ?? 0,
                    StateName = order.State?.Name,
                    UserName = order.Users?.Name
                };
                
                clientServiceOrderDtos.Add(dto);
            }
            
            return clientServiceOrderDtos;
        }

        public async Task<bool> AuthorizeServiceOrderAsync(int serviceOrderId, bool isAuthorized, string? clientMessage)
        {
            return await _unitOfWork.ServiceOrderRepository.UpdateServiceOrderAuthorizationAsync(serviceOrderId, isAuthorized, clientMessage);
        }
    }
} 