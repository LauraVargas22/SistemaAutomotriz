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

        [HttpGet("{diagnosticId:int}/{serviceOrderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DetailsDiagnosticDto>> Get(int diagnosticId, int serviceOrderId)
        {
            var detailDiagnostic = await _unitOfWork.DetailsDiagnosticRepository.GetByIdsAsync(diagnosticId, serviceOrderId);
            if (detailDiagnostic == null)
            {
                return NotFound($"Details Diagnostic with keys ({diagnosticId}, {serviceOrderId}) not found.");
            }
            return _mapper.Map<DetailsDiagnosticDto>(detailDiagnostic);
        }

        [HttpPut("{diagnosticId:int}/{serviceOrderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int diagnosticId, int serviceOrderId, [FromBody] DetailsDiagnosticDto detailsDiagnosticDto)
        {
            if (detailsDiagnosticDto == null || detailsDiagnosticDto.Diagnostic_Id != diagnosticId || detailsDiagnosticDto.ServiceOrder_Id != serviceOrderId)
            {
                return BadRequest("Mismatched or invalid data.");
            }
            var existing = await _unitOfWork.DetailsDiagnosticRepository.GetByIdsAsync(diagnosticId, serviceOrderId);
            if (existing == null)
                return NotFound();
            var detailsDiagnostic = _mapper.Map<DetailsDiagnostic>(detailsDiagnosticDto);
            _unitOfWork.DetailsDiagnosticRepository.Update(detailsDiagnostic);
            await _unitOfWork.SaveAsync();
            return Ok(detailsDiagnosticDto);
        }

        [HttpDelete("{diagnosticId:int}/{serviceOrderId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int diagnosticId, int serviceOrderId)
        {
            var detailsDiagnostic = await _unitOfWork.DetailsDiagnosticRepository.GetByIdsAsync(diagnosticId, serviceOrderId);
            if (detailsDiagnostic == null)
                return NotFound();

            _unitOfWork.DetailsDiagnosticRepository.Remove(detailsDiagnostic);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}