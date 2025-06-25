using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Application.Services;

namespace Application.Services
{
    public class DeleteServiceOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int serviceOrderId)
        {
            // 1. Obtener la orden de servicio
            var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(serviceOrderId)
                ?? throw new Exception("Orden de servicio no encontrada.");

            // 2. Obtener y eliminar los detalles asociados
            var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByServiceOrderIdAsync(serviceOrderId);

            foreach (var detail in orderDetails)
            {
                // 2.1 Devolver el stock
                var sparePart = await _unitOfWork.SparePartRepository.GetByIdAsync(detail.SparePartId);
                if (sparePart != null)
                {
                    sparePart.Stock += detail.RequiredPieces;
                    _unitOfWork.SparePartRepository.Update(sparePart);
                }

                _unitOfWork.OrderDetailsRepository.Remove(detail);
            }

            // 3. Eliminar la orden de servicio
            _unitOfWork.ServiceOrderRepository.Remove(serviceOrder);

            // 4. Guardar cambios
            await _unitOfWork.SaveAsync();
        }
    }
}
