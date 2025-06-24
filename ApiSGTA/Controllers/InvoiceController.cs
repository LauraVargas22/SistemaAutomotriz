using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ApiSGTA.Controllers;
using Application.DTOs;
using AutoMapper;
using Application.Services;
using ApiSGTA.Helpers.Errors;

namespace ApiSGTA.Controllers
{
    public class InvoiceController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GenerateInvoice _generateInovice;

        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper, GenerateInvoice generateInovice)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _generateInovice = generateInovice;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> Get()
        {
            var invoices = await _unitOfWork.InvoiceRepository.GetAllAsync();
            return _mapper.Map<List<InvoiceDto>>(invoices);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvoiceDto>> Get(int id)
        {
            var invoices = await _unitOfWork.InvoiceRepository.GetByIdAsync(id);
            if (invoices == null)
            {
                return NotFound($"Invoices with id {id} was not found.");
            }
            return _mapper.Map<InvoiceDto>(invoices);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Invoice>> Post(InvoiceDto invoiceDto)
        {
            var invoices = _mapper.Map<Invoice>(invoiceDto);
            _unitOfWork.InvoiceRepository.Add(invoices);
            await _unitOfWork.SaveAsync();
            if (invoiceDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = invoiceDto.Id }, invoices);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] InvoiceDto invoiceDto)
        {
            // Validación: objeto nulo
            if (invoiceDto == null)
                return NotFound();

            var invoices = _mapper.Map<Invoice>(invoiceDto);
            _unitOfWork.InvoiceRepository.Update(invoices);
            await _unitOfWork.SaveAsync();
            return Ok(invoiceDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var invoicesw = await _unitOfWork.InvoiceRepository.GetByIdAsync(id);
            if (invoicesw == null)
                return NotFound();

            _unitOfWork.InvoiceRepository.Remove(invoicesw);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpPost("generate/{serviceOrderId}")]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<IActionResult> Generate(int serviceOrderId)
{
    try
    {
        var invoiceDto = await _generateInovice.ExecuteAsync(serviceOrderId);
        return CreatedAtAction(nameof(Get), new { id = invoiceDto.Id }, invoiceDto);
    }
    catch (Exception ex)
    {
        if (ex.Message.Contains("not found"))
            return NotFound(new ApiResponse(404, ex.Message));
        if (ex.Message.Contains("No order details"))
            return BadRequest(new ApiResponse(400, ex.Message));
        return StatusCode(500, new ApiResponse(500, ex.Message));
    }
}
    }
}