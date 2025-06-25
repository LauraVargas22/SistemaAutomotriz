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
    public class InspectionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InspectionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InspectionDto>>> Get()
        {
            var inspections = await _unitOfWork.InspectionRepository.GetAllAsync();
            return _mapper.Map<List<InspectionDto>>(inspections);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InspectionDto>> Get(int id)
        {
            var inspections = await _unitOfWork.InspectionRepository.GetByIdAsync(id);
            if (inspections == null)
            {
                return NotFound($"Inspections with id {id} was not found.");
            }
            return _mapper.Map<InspectionDto>(inspections);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Inspection>> Post(InspectionDto inspectionDto)
        {
            var inspections = _mapper.Map<Inspection>(inspectionDto);
            _unitOfWork.InspectionRepository.Add(inspections);
            await _unitOfWork.SaveAsync();
            if (inspectionDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = inspectionDto.Id }, inspections);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] InspectionDto inspectionDto)
        {
            // Validación: objeto nulo
            if (inspectionDto == null)
                return NotFound();

            var inspections = _mapper.Map<Inspection>(inspectionDto);
            _unitOfWork.InspectionRepository.Update(inspections);
            await _unitOfWork.SaveAsync();
            return Ok(inspectionDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var inspections = await _unitOfWork.InspectionRepository.GetByIdAsync(id);
            if (inspections == null)
                return NotFound();

            _unitOfWork.InspectionRepository.Remove(inspections);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}