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
    public class DetailsDiagnosticController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DetailsDiagnosticController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DetailsDiagnostic>>> Get()
        {
            var detailsDiagnostic = await _unitOfWork.DetailsDiagnosticRepository.GetAllAsync();
            return _mapper.Map<List<DetailsDiagnostic>>(detailsDiagnostic);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetailsDiagnosticDto>> Get(int id)
        {
            var detailDiagnostic = await _unitOfWork.DetailsDiagnosticRepository.GetByIdAsync(id);
            if (detailDiagnostic == null)
            {
                return NotFound($"Detail Diagnostic with id {id} was not found.");
            }
            return _mapper.Map<DetailsDiagnosticDto>(detailDiagnostic);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] DetailsDiagnosticDto detailsDiagnosticDto)
        {
            // Validación: objeto nulo
            if (detailsDiagnosticDto == null)
                return NotFound();

            var detailsDiagnostic = _mapper.Map<DetailsDiagnostic>(detailsDiagnosticDto);
            _unitOfWork.DetailsDiagnosticRepository.Update(detailsDiagnostic);
            await _unitOfWork.SaveAsync();
            return Ok(detailsDiagnosticDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var detailsDiagnostic = await _unitOfWork.DetailsDiagnosticRepository.GetByIdAsync(id);
            if (detailsDiagnostic == null)
                return NotFound();

            _unitOfWork.DetailsDiagnosticRepository.Remove(detailsDiagnostic);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}