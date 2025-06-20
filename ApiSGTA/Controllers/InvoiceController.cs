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
    public class InvoiceController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}