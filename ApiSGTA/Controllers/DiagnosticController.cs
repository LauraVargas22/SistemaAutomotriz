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
    public class DiagnosticController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiagnosticController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DiagnosticDto>>> Get()
        {
            var diagnostics = await _unitOfWork.DiagnosticRepository.GetAllAsync();
            return _mapper.Map<List<DiagnosticDto>>(diagnostics);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DiagnosticDto>> Get(int id)
        {
            var diagnostics = await _unitOfWork.DiagnosticRepository.GetByIdAsync(id);
            if (diagnostics == null)
            {
                return NotFound($"Diagnostic with id {id} was not found.");
            }
            return _mapper.Map<DiagnosticDto>(diagnostics);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Diagnostic>> Post(DiagnosticDto diagnostic)
        {
            var diagnostics = _mapper.Map<Diagnostic>(diagnostic);
            _unitOfWork.DiagnosticRepository.Add(diagnostics);
            await _unitOfWork.SaveAsync();
            if (diagnostic == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = diagnostic.Id }, diagnostics);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] DiagnosticDto diagnosticDto)
        {
            // Validación: objeto nulo
            if (diagnosticDto == null)
                return NotFound();

            var diagnostics = _mapper.Map<Diagnostic>(diagnosticDto);
            _unitOfWork.DiagnosticRepository.Update(diagnostics);
            await _unitOfWork.SaveAsync();
            return Ok(diagnosticDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var diagnostic = await _unitOfWork.DiagnosticRepository.GetByIdAsync(id);
            if (diagnostic == null)
                return NotFound();

            _unitOfWork.DiagnosticRepository.Remove(diagnostic);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}