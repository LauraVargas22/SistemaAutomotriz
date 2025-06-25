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
    public class TypeServiceController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TypeServiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TypeServiceDto>>> Get()
        {
            var typeServices = await _unitOfWork.TypeServiceRepository.GetAllAsync();
            return _mapper.Map<List<TypeServiceDto>>(typeServices);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeServiceDto>> Get(int id)
        {
            var typeServices = await _unitOfWork.TypeServiceRepository.GetByIdAsync(id);
            if (typeServices == null)
            {
                return NotFound($"Type Service with id {id} was not found.");
            }
            return _mapper.Map<TypeServiceDto>(typeServices);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeService>> Post(TypeServiceDto typeServiceDto)
        {
            var typeServices = _mapper.Map<TypeService>(typeServiceDto);
            _unitOfWork.TypeServiceRepository.Add(typeServices);
            await _unitOfWork.SaveAsync();
            if (typeServiceDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = typeServiceDto.Id }, typeServices);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] TypeServiceDto typeServiceDto)
        {
            if (typeServiceDto == null)
                return NotFound();

            var typeService = _mapper.Map<TypeService>(typeServiceDto);
            _unitOfWork.TypeServiceRepository.Update(typeService);
            await _unitOfWork.SaveAsync();
            return Ok(typeServiceDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var typeService = await _unitOfWork.TypeServiceRepository.GetByIdAsync(id);
            if (typeService == null)
                return NotFound();

            _unitOfWork.TypeServiceRepository.Remove(typeService);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}