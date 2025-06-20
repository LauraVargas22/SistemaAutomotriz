using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ApiSGTA.Controllers;
using Application.DTOs;
using AutoMapper;

namespace ApiSGTA.Controllers
{
    public class ServiceOrderController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceOrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}