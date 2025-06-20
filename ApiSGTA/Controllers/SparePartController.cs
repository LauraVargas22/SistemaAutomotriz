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
    }
}