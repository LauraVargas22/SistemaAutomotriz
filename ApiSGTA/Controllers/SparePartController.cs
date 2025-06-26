using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiSGTA.Controllers;
using Application.DTOs;
using AutoMapper;
using ApiSGTA.Helpers.Errors;

namespace ApiSGTA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class SparePartController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SparePartController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SparePartDto>>> Get()
        {
            var spareParts = await _unitOfWork.SparePartRepository.GetAllAsync();
            return _mapper.Map<List<SparePartDto>>(spareParts);
        }

        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SparePartDto>>> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "")
        {
            var (totalRegisters, registers) = await _unitOfWork.SparePartRepository.GetAllAsync(pageNumber, pageSize, search);
            var sparePartDtos = _mapper.Map<List<SparePartDto>>(registers);

            // Agregar X-Total-Count en los encabezados HTTP
            Response.Headers.Append("X-Total-Count", totalRegisters.ToString());

            return Ok(sparePartDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SparePartDto>> Get(int id)
        {
            var sparePart = await _unitOfWork.SparePartRepository.GetByIdAsync(id);
            if (sparePart == null)
            {
                return NotFound($"Spare Part with id {id} was not found.");
            }
            return _mapper.Map<SparePartDto>(sparePart);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SparePart>> Post(SparePartDto sparePartDto)
        {
            var sparePart = _mapper.Map<SparePart>(sparePartDto);
            _unitOfWork.SparePartRepository.Add(sparePart);
            await _unitOfWork.SaveAsync();
            if (sparePartDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = sparePartDto.Id }, sparePart);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] SparePartDto sparePartDto)
        {
            if (sparePartDto == null)
                return NotFound();

            var sparePart = _mapper.Map<SparePart>(sparePartDto);
            _unitOfWork.SparePartRepository.Update(sparePart);
            await _unitOfWork.SaveAsync();
            return Ok(sparePartDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var sparePart = await _unitOfWork.SparePartRepository.GetByIdAsync(id);
            if (sparePart == null)
                return NotFound();

            _unitOfWork.SparePartRepository.Remove(sparePart);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Alta de repuestos agregar nuevo repuesto al inventario
        /// </summary>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSparePart([FromBody] SparePartDto sparePartDto)
        {
            if (sparePartDto == null)
                return BadRequest(new ApiResponse(400, "Datos de repuesto inválidos."));

            var sparePart = _mapper.Map<SparePart>(sparePartDto);
            _unitOfWork.SparePartRepository.Add(sparePart);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction(nameof(Get), new { id = sparePart.Id }, _mapper.Map<SparePartDto>(sparePart));
        }

        /// <summary>
        /// Actualización de stock de un repuesto
        /// </summary>
        [HttpPatch("{id}/stock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] int newStock)
        {
            var sparePart = await _unitOfWork.SparePartRepository.GetByIdAsync(id);
            if (sparePart == null)
                return NotFound(new ApiResponse(404, $"Spare Part with id {id} was not found."));

            sparePart.Stock = newStock;
            _unitOfWork.SparePartRepository.Update(sparePart);
            await _unitOfWork.SaveAsync();

            return Ok(_mapper.Map<SparePartDto>(sparePart));
        }

        /// <summary>
        /// Baja de repuestos obsoletos eliminar repuesto
        /// </summary>
        [HttpDelete("obsolete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteObsolete(int id)
        {
            var sparePart = await _unitOfWork.SparePartRepository.GetByIdAsync(id);
            if (sparePart == null)
                return NotFound(new ApiResponse(404, $"Spare Part with id {id} was not found."));

            _unitOfWork.SparePartRepository.Remove(sparePart);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

    }
}