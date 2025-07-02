using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports;
using Domain.Entities;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class CreateEmailServiceOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public CreateEmailServiceOrderService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<ServiceOrderDto> CreateEmailServiceOrderAsync(ServiceOrderDto serviceOrderDto)
        {
            // 1. Create the ServiceOrder entity
            var serviceOrder = new ServiceOrder
            {
                VehiclesId = serviceOrderDto.VehiclesId,
                TypeServiceId = serviceOrderDto.TypeServiceId,
                StateId = serviceOrderDto.StateId,
                EntryDate = serviceOrderDto.EntryDate,
                ExitDate = serviceOrderDto.ExitDate,
                IsAuthorized = serviceOrderDto.IsAuthorized,
                ClientMessage = serviceOrderDto.ClientMessage,
                UserId = serviceOrderDto.UserId
            };

            _unitOfWork.ServiceOrderRepository.Add(serviceOrder);
            await _unitOfWork.SaveAsync();

            // 2. Fetch the Vehicle and Client
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(serviceOrder.VehiclesId)
                ?? throw new Exception("Vehículo no encontrado");

            var client = await _unitOfWork.ClientRepository.GetByIdAsync(vehicle.ClientId)
                ?? throw new Exception("Cliente no encontrado");

            // 3. Send the email
            await _emailService.SendServiceOrderCreatedEmailAsync(
                client.Email,
                $"{client.Name} {client.LastName}",
                serviceOrder.Id
            );

            // 4. Map to DTO and return
            return new ServiceOrderDto
            {
                Id = serviceOrder.Id,
                VehiclesId = serviceOrder.VehiclesId,
                TypeServiceId = serviceOrder.TypeServiceId,
                StateId = serviceOrder.StateId,
                EntryDate = serviceOrder.EntryDate ?? default,
                ExitDate = serviceOrder.ExitDate ?? default,
                IsAuthorized = serviceOrder.IsAuthorized,
                ClientMessage = serviceOrder.ClientMessage,
                UserId = serviceOrder.UserId
            };
        }
    }
}