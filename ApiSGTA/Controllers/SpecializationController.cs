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
    public class SpecializationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SpecializationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SpecializationDto>>> Get()
        {
            var specializations = await _unitOfWork.SpecializationRepository.GetAllAsync();
            return _mapper.Map<List<SpecializationDto>>(specializations);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpecializationDto>> Get(int id)
        {
            var specialization = await _unitOfWork.SpecializationRepository.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound($"Specialization with id {id} was not found.");
            }
            return _mapper.Map<SpecializationDto>(specialization);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Specialization>> Post(SpecializationDto specializationDto)
        {
            var specialization = _mapper.Map<Specialization>(specializationDto);
            _unitOfWork.SpecializationRepository.Add(specialization);
            await _unitOfWork.SaveAsync();
            if (specializationDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = specializationDto.Id }, specialization);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SpecializationDto specializationDto)
        {
            if (specializationDto == null)
                return NotFound();

            var specialization = _mapper.Map<Specialization>(specializationDto);
            _unitOfWork.SpecializationRepository.Update(specialization);
            await _unitOfWork.SaveAsync();
            return Ok(specializationDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var specialization = await _unitOfWork.SpecializationRepository.GetByIdAsync(id);
            if (specialization == null)
                return NotFound();

            _unitOfWork.SpecializationRepository.Remove(specialization);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}