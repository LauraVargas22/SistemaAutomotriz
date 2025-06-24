using System;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class GenerateInvoice
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateInvoice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<InvoiceDto> ExecuteAsync(int serviceOrderId)
        {
            // 1. Obtener la orden de servicio
            var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(serviceOrderId);
            if (serviceOrder == null)
                throw new Exception($"Service Order with id {serviceOrderId} not found");

            // 2. Obtener los detalles de la orden
            var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByServiceOrderIdAsync(serviceOrderId);
            if (orderDetails == null || !orderDetails.Any())
                throw new Exception("No order details found for this service order");

            // 3. Calcular el total de la factura
            decimal total = 0;
            foreach (var detail in orderDetails)
            {
                var sparePart = await _unitOfWork.SparePartRepository.GetByIdAsync(detail.SparePartId);
                if (sparePart == null)
                    throw new Exception($"Spare part with id {detail.SparePartId} not found");

                total += sparePart.UnitPrice * detail.RequiredPieces;
            }

            // 4. Crear la entidad Invoice y guardarla
            var invoice = new Invoice
            {
                ServiceOrderId = serviceOrderId,
                TotalPrice = total,
                Date = DateTime.Now,
                Code = Guid.NewGuid().ToString().Substring(0, 8)
            };

            _unitOfWork.InvoiceRepository.Add(invoice);
            await _unitOfWork.SaveAsync();

            // 5. Devolver el DTO
            return new InvoiceDto
            {
                Id = invoice.Id,
                ServiceOrder_Id = invoice.ServiceOrderId,
                TotalPrice = invoice.TotalPrice,
                Date = invoice.Date,
                Code = invoice.Code
            };
        }
    }
}