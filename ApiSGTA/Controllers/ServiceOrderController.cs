using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiSGTA.Controllers;
using Application.DTOs;
using AutoMapper;
using Application.Services;
using ApiSGTA.Helpers.Errors;
using Application.DTOs.CreateServiceOrderDto;

namespace ApiSGTA.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, Recepcionist, Mechanic")]
    public class ServiceOrderController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RegisterOrderDetailsService _registerOrderDetails;
        private readonly CreateServiceOrderService _createServiceOrderService;
        private readonly UpdateServiceOrderService _updateServiceOrderService;
        private readonly DeleteServiceOrderService _deleteServiceOrderService;
        private readonly CreateEmailServiceOrderService _createService;

        public ServiceOrderController(IUnitOfWork unitOfWork, IMapper mapper, RegisterOrderDetailsService registerOrderDetails, CreateServiceOrderService createServiceOrderService, UpdateServiceOrderService updateServiceOrderService, DeleteServiceOrderService deleteServiceOrderService, CreateEmailServiceOrderService createService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _registerOrderDetails = registerOrderDetails;
            _createServiceOrderService = createServiceOrderService;
            _updateServiceOrderService = updateServiceOrderService;
            _deleteServiceOrderService = deleteServiceOrderService;
            _createService = createService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> Get()
        {
            var serviceOrders = await _unitOfWork.ServiceOrderRepository.GetAllAsync();
            return _mapper.Map<List<ServiceOrderDto>>(serviceOrders);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ServiceOrderDto>>> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "")
        {
            var (totalRegisters, registers) = await _unitOfWork.ServiceOrderRepository.GetAllAsync(pageNumber, pageSize, search);
            var serviceOrderDtos = _mapper.Map<List<ServiceOrderDto>>(registers);
            
            // Agregar X-Total-Count en los encabezados HTTP
            Response.Headers.Append("X-Total-Count", totalRegisters.ToString());
            
            return Ok(serviceOrderDtos);
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

        // // Implementation of CreateServiceOrderService
        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status201Created)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<IActionResult> Post([FromBody] CreateServiceOrderDto dto)
        // {
        //     try
        //     {
        //         var newOrderId = await _createServiceOrderService.ExecuteAsync(dto);
        //         return CreatedAtAction(nameof(Get), new { id = newOrderId }, new { id = newOrderId });
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new ApiResponse(400, ex.Message));
        //     }
        // }

        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<ActionResult<ServiceOrder>> Post(ServiceOrderDto serviceOrderDto)
        // {
        //     var serviceOrder = _mapper.Map<ServiceOrder>(serviceOrderDto);
        //     _unitOfWork.ServiceOrderRepository.Add(serviceOrder);
        //     await _unitOfWork.SaveAsync();
        //     if (serviceOrderDto == null)
        //     {
        //         return BadRequest();
        //     }
        //     return CreatedAtAction(nameof(Post), new { id = serviceOrderDto.Id }, serviceOrder);
        // }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceOrderDto request)
        {
            try
            {
                var result = await _createService.CreateEmailServiceOrderAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Implementation of UpdateServiceOrderService
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateServiceOrderDto dto)
        {
            if (dto == null || id != dto.Id)
                return BadRequest(new ApiResponse(400, "ID inválido."));

            try
            {
                await _updateServiceOrderService.ExecuteAsync(dto);
                return Ok(new { message = "Orden de servicio actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse(404, ex.Message));
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _deleteServiceOrderService.ExecuteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse(404, ex.Message));
            }
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