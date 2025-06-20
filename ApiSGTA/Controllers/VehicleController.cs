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
    public class VehicleController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> Get()
        {
            var vehicles = await _unitOfWork.VehicleRepository.GetAllAsync();
            return _mapper.Map<List<VehicleDto>>(vehicles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VehicleDto>> Get(int id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound($"Vehicle with id {id} was not found.");
            }
            return _mapper.Map<VehicleDto>(vehicle);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Vehicle>> Post(VehicleDto vehicleDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleDto);
            _unitOfWork.VehicleRepository.Add(vehicle);
            await _unitOfWork.SaveAsync();
            if (vehicleDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = vehicleDto.Id }, vehicle);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
                return NotFound();

            var vehicle = _mapper.Map<Vehicle>(vehicleDto);
            _unitOfWork.VehicleRepository.Update(vehicle);
            await _unitOfWork.SaveAsync();
            return Ok(vehicleDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return NotFound();

            _unitOfWork.VehicleRepository.Remove(vehicle);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}