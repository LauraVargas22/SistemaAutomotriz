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
    public class TypeVehicleController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TypeVehicleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TypeVehicleDto>>> Get()
        {
            var typeVehicles = await _unitOfWork.TypeVehicleRepository.GetAllAsync();
            return _mapper.Map<List<TypeVehicleDto>>(typeVehicles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeVehicleDto>> Get(int id)
        {
            var typeVehicles = await _unitOfWork.TypeVehicleRepository.GetByIdAsync(id);
            if (typeVehicles == null)
            {
                return NotFound($"Type vehicle with id {id} was not found.");
            }
            return _mapper.Map<TypeVehicleDto>(typeVehicles);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeVehicle>> Post(TypeVehicleDto typeVehicleDto)
        {
            var typeVehicles = _mapper.Map<TypeVehicle>(typeVehicleDto);
            _unitOfWork.TypeVehicleRepository.Add(typeVehicles);
            await _unitOfWork.SaveAsync();
            if (typeVehicleDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = typeVehicleDto.Id }, typeVehicles);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] TypeVehicleDto typeVehicleDto)
        {
            if (typeVehicleDto == null)
                return NotFound();

            var typeVehicle = _mapper.Map<TypeVehicle>(typeVehicleDto);
            _unitOfWork.TypeVehicleRepository.Update(typeVehicle);
            await _unitOfWork.SaveAsync();
            return Ok(typeVehicleDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var typeVehicle = await _unitOfWork.TypeVehicleRepository.GetByIdAsync(id);
            if (typeVehicle == null)
                return NotFound();

            _unitOfWork.TypeVehicleRepository.Remove(typeVehicle);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}