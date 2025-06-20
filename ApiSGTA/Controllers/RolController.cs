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
    public class RolController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RolDto>>> Get()
        {
            var rols = await _unitOfWork.RolRepository.GetAllAsync();
            return _mapper.Map<List<RolDto>>(rols);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RolDto>> Get(int id)
        {
            var rol = await _unitOfWork.RolRepository.GetByIdAsync(id);
            if (rol == null)
            {
                return NotFound($"Rol with id {id} was not found.");
            }
            return _mapper.Map<RolDto>(rol);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rol>> Post(RolDto rolDto)
        {
            var rol = _mapper.Map<Rol>(rolDto);
            _unitOfWork.RolRepository.Add(rol);
            await _unitOfWork.SaveAsync();
            if (rolDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = rolDto.Id }, rol);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] RolDto rolDto)
        {
            if (rolDto == null)
                return NotFound();

            var rol = _mapper.Map<Rol>(rolDto);
            _unitOfWork.RolRepository.Update(rol);
            await _unitOfWork.SaveAsync();
            return Ok(rolDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _unitOfWork.RolRepository.GetByIdAsync(id);
            if (rol == null)
                return NotFound();

            _unitOfWork.RolRepository.Remove(rol);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}