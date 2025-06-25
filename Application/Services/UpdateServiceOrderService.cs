using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class UpdateServiceOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateServiceOrderDto dto)
        {
            var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(dto.Id)
                ?? throw new Exception("Orden de servicio no encontrada.");

            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(dto.VehiclesId)
                ?? throw new Exception("Vehículo no encontrado.");

            var typeService = await _unitOfWork.TypeServiceRepository.GetByIdAsync(dto.TypeServiceId)
                ?? throw new Exception("Tipo de servicio no encontrado.");

            var state = await _unitOfWork.StateRepository.GetByIdAsync(dto.StateId)
                ?? throw new Exception("Estado inválido.");

            // Actualización de campos
            serviceOrder.VehiclesId = dto.VehiclesId;
            serviceOrder.TypeServiceId = dto.TypeServiceId;
            serviceOrder.StateId = dto.StateId;
            serviceOrder.EntryDate = dto.EntryDate;
            serviceOrder.ExitDate = dto.EntryDate.AddDays(typeService.Duration);
            serviceOrder.ClientMessage = dto.ClientMessage;

            _unitOfWork.ServiceOrderRepository.Update(serviceOrder);
            await _unitOfWork.SaveAsync();
        }
    }
}
