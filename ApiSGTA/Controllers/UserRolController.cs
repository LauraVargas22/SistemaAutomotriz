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
    public class UserRolController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserRolController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserRolDto>>> Get()
        {
            var userRols = await _unitOfWork.UserRolRepository.GetAllAsync();
            return _mapper.Map<List<UserRolDto>>(userRols);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserRolDto>> Get(int id)
        {
            var userRol = await _unitOfWork.UserRolRepository.GetByIdAsync(id);
            if (userRol == null)
            {
                return NotFound($"UserRol with id {id} was not found.");
            }
            return _mapper.Map<UserRolDto>(userRol);
        }

        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<ActionResult<UserRol>> Post(UserRolDto userRolDto)
        // {
        //     var userRol = _mapper.Map<UserRol>(userRolDto);
        //     _unitOfWork.UserRolRepository.Add(userRol);
        //     await _unitOfWork.SaveAsync();
        //     if (userRolDto == null)
        //     {
        //         return BadRequest();
        //     }
        //     return CreatedAtAction(nameof(Post), new { id = userRolDto.Id }, userRol);
        // }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] UserRolDto userRolDto)
        {
            if (userRolDto == null)
                return NotFound();

            var userRol = _mapper.Map<UserRol>(userRolDto);
            _unitOfWork.UserRolRepository.Update(userRol);
            await _unitOfWork.SaveAsync();
            return Ok(userRolDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userRol = await _unitOfWork.UserRolRepository.GetByIdAsync(id);
            if (userRol == null)
                return NotFound();

            _unitOfWork.UserRolRepository.Remove(userRol);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}