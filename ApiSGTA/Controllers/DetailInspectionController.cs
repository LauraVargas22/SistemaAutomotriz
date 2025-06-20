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
    public class DetailInspectionController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DetailInspectionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DetailInspectionDto>>> Get()
        {
            var detailsInspection = await _unitOfWork.DetailsDiagnosticRepository.GetAllAsync();
            return _mapper.Map<List<DetailInspectionDto>>(detailsInspection);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetailInspectionDto>> Get(int id)
        {
            var detailsInspection = await _unitOfWork.DetailInspectionRepository.GetByIdAsync(id);
            if (detailsInspection == null)
            {
                return NotFound($"Details Inspection with id {id} was not found.");
            }
            return _mapper.Map<DetailInspectionDto>(detailsInspection);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetailInspection>> Post(DetailInspectionDto detailsInspectionDto)
        {
            var detailsInspections = _mapper.Map<DetailInspection>(detailsInspectionDto);
            _unitOfWork.DetailInspectionRepository.Add(detailsInspections);
            await _unitOfWork.SaveAsync();
            if (detailsInspectionDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = detailsInspectionDto.Id }, detailsInspections);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] DetailInspectionDto detailInspectionDto)
        {
            // Validación: objeto nulo
            if (detailInspectionDto == null)
                return NotFound();

            var detailsInspections = _mapper.Map<DetailInspection>(detailInspectionDto);
            _unitOfWork.DetailInspectionRepository.Update(detailsInspections);
            await _unitOfWork.SaveAsync();
            return Ok(detailInspectionDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var detailsInspection = await _unitOfWork.DetailInspectionRepository.GetByIdAsync(id);
            if (detailsInspection == null)
                return NotFound();

            _unitOfWork.DetailInspectionRepository.Remove(detailsInspection);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}