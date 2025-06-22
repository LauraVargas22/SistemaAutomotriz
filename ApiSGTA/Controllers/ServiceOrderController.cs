using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ApiSGTA.Controllers;
using Application.DTOs;
using AutoMapper;
using Application.Services;
using ApiSGTA.Helpers.Errors;

namespace ApiSGTA.Controllers
{
    public class ServiceOrderController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RegisterOrderDetailsService _registerOrderDetails;

        public ServiceOrderController(IUnitOfWork unitOfWork, IMapper mapper, RegisterOrderDetailsService registerOrderDetails)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _registerOrderDetails = registerOrderDetails;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> Get()
        {
            var serviceOrders = await _unitOfWork.ServiceOrderRepository.GetAllAsync();
            return _mapper.Map<List<ServiceOrderDto>>(serviceOrders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceOrderDto>> Get(int id)
        {
            var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(id);
            if (serviceOrder == null)
            {
                return NotFound($"Service Order with id {id} was not found.");
            }
            return _mapper.Map<ServiceOrderDto>(serviceOrder);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServiceOrder>> Post(ServiceOrderDto serviceOrderDto)
        {
            var serviceOrder = _mapper.Map<ServiceOrder>(serviceOrderDto);
            _unitOfWork.ServiceOrderRepository.Add(serviceOrder);
            await _unitOfWork.SaveAsync();
            if (serviceOrderDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = serviceOrderDto.Id }, serviceOrder);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] ServiceOrderDto serviceOrderDto)
        {
            if (serviceOrderDto == null)
                return NotFound();

            var serviceOrder = _mapper.Map<ServiceOrder>(serviceOrderDto);
            _unitOfWork.ServiceOrderRepository.Update(serviceOrder);
            await _unitOfWork.SaveAsync();
            return Ok(serviceOrderDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceOrder = await _unitOfWork.ServiceOrderRepository.GetByIdAsync(id);
            if (serviceOrder == null)
                return NotFound();

            _unitOfWork.ServiceOrderRepository.Remove(serviceOrder);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        //Requests to use the service RegisterOrderService
        [HttpPost("{serviceOrderId}/details")]
        public async Task<IActionResult> AddOrderDetail(int serviceOrderId, [FromBody] OrderDetailsDto orderDetail)
        {
            try
            {
                await _registerOrderDetails.RegisterOrderDetailAsync(
                    serviceOrderId,
                    orderDetail.SparePartId,
                    orderDetail.RequiredPieces
                );
                return Ok("Order detail added successfully.");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound(new ApiResponse(404, ex.Message));
                }
                if (ex.Message.Contains("Insufficient stock"))
                {
                    return BadRequest(new ApiResponse(400, ex.Message));
                }
                return StatusCode(500, new ApiResponse(500, ex.Message));
            }
        }

        [HttpPatch("{serviceOrderId}/state")]
        public async Task<IActionResult> UpdateOrderState(int serviceOrderId, [FromBody] UpdateStateDto stateDto)
        {
            try
            {
                await _registerOrderDetails.UpdateStateServiceOrder(serviceOrderId, stateDto.StateId);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                {
                    return NotFound(new ApiResponse(404, ex.Message));
                }
                return StatusCode(500, new ApiResponse(500, ex.Message));
            }
        }
    }
}