using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class RegisterOrderDetailsService
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterOrderDetailsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterOrderDetailAsync(int serviceOrderId, int sparePartId, int requiredPieces)
    {
        var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(serviceOrderId);
        if (serviceOrder == null)
        {
            throw new Exception($"Service Order with id {serviceOrderId} not found");
        }

        var sparePart = await _unitOfWork.SparePartRepository.GetByIdAsync(sparePartId);
        if (sparePart == null)
        {
            throw new Exception($"Spare part with id {sparePartId} not found");
        }

        if (sparePart.Stock < requiredPieces)
        {
            throw new Exception($"Insufficient stock for spare part {sparePart.Description}. Available: {sparePart.Stock}, Required: {requiredPieces}");
        }

        var totalPrice = sparePart.UnitPrice * requiredPieces;

        var orderDetail = new OrderDetails
        {
            ServiceOrderId = serviceOrderId,
            SparePartId = sparePartId,
            RequiredPieces = requiredPieces,
            TotalPrice = totalPrice
        };

        _unitOfWork.OrderDetailsRepository.Add(orderDetail);

        sparePart.Stock -= requiredPieces;
        _unitOfWork.SparePartRepository.Update(sparePart);

        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateStateServiceOrder(int serviceOrderId, int stateId)
    {
        var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(serviceOrderId);
        if (serviceOrder == null)
        {
            throw new Exception($"Service Order with id {serviceOrderId} not found");
        }

        var state = await _unitOfWork.StateRepository.GetByIdAsync(stateId);
        if (state == null)
        {
            throw new Exception($"State with id {stateId} not found");
        }

        serviceOrder.StateId = stateId;
        _unitOfWork.ServiceOrderRepository.Update(serviceOrder);
        
        await _unitOfWork.SaveAsync();
    }
}
