using System;
using System.Threading.Tasks;
using Domain.Entities;
using Application.DTOs.CreateServiceOrderDto;
using Application.Interfaces;

namespace Application.Services
{
    public class CreateServiceOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> ExecuteAsync(CreateServiceOrderDto dto)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(dto.Vehicle_id)
                ?? throw new Exception("Vehículo no encontrado");

            var typeService = await _unitOfWork.TypeServiceRepository.GetByIdAsync(dto.Type_service_id)
                ?? throw new Exception("Tipo de servicio no encontrado");

            var client = await _unitOfWork.ClientRepository.GetByIdAsync(dto.Client_id)
                ?? throw new Exception("Cliente no encontrado");

            var state = await _unitOfWork.StateRepository.GetByIdAsync(dto.State_id)
                ?? throw new Exception("Estado inválido");

            var exitDate = dto.Entry_date.AddDays(typeService.Duration);

            var serviceOrder = new ServiceOrder
            {
                VehiclesId = dto.Vehicle_id,
                TypeServiceId = dto.Type_service_id,
                StateId = dto.State_id,
                EntryDate = dto.Entry_date,
                ExitDate = exitDate,
                IsAuthorized = false,
                ClientMessage = dto.Client_message
            };

            _unitOfWork.ServiceOrderRepository.Add(serviceOrder);
            await _unitOfWork.SaveAsync();

            foreach (var detail in dto.order_details)
            {
                var part = await _unitOfWork.SparePartRepository.GetByIdAsync(detail.space_part_id)
                    ?? throw new Exception("Repuesto no encontrado");

                if (part.Stock < detail.required_pieces)
                    throw new Exception($"Stock insuficiente para {part.Description}");

                part.Stock -= detail.required_pieces;

                var orderDetail = new OrderDetails
                {
                    ServiceOrderId = serviceOrder.Id,
                    SparePartId = detail.space_part_id,
                    RequiredPieces = detail.required_pieces,
                    TotalPrice = part.UnitPrice * detail.required_pieces
                };

                _unitOfWork.OrderDetailsRepository.Add(orderDetail);
            }

            await _unitOfWork.SaveAsync();

            return serviceOrder.Id;
        }
    }
}
