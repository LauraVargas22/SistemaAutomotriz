using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiSGTA.Controllers;
using Application.DTOs;
using AutoMapper;

namespace ApiSGTA.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, Mechanic")]
    public class OrderDetailsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDetailsDto>>> Get()
        {
            var orderDetailsList = await _unitOfWork.OrderDetailsRepository.GetAllAsync();
            return _mapper.Map<List<OrderDetailsDto>>(orderDetailsList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailsDto>> Get(int id)
        {
            var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id);
            if (orderDetails == null)
            {
                return NotFound($"Order Details with id {id} was not found.");
            }
            return _mapper.Map<OrderDetailsDto>(orderDetails);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetails>> Post(OrderDetailsDto orderDetailsDto)
        {
            var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);
            _unitOfWork.OrderDetailsRepository.Add(orderDetails);
            await _unitOfWork.SaveAsync();
            if (orderDetailsDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = orderDetailsDto.Id }, orderDetails);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] OrderDetailsDto orderDetailsDto)
        {
            if (orderDetailsDto == null)
                return NotFound();

            var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);
            _unitOfWork.OrderDetailsRepository.Update(orderDetails);
            await _unitOfWork.SaveAsync();
            return Ok(orderDetailsDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetails = await _unitOfWork.OrderDetailsRepository.GetByIdAsync(id);
            if (orderDetails == null)
                return NotFound();

            _unitOfWork.OrderDetailsRepository.Remove(orderDetails);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}