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
    public class UserSpecializationController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserSpecializationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserSpecializationDto>>> Get()
        {
            var userSpecializations = await _unitOfWork.UserSpessializationRepository.GetAllAsync();
            return _mapper.Map<List<UserSpecializationDto>>(userSpecializations);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserSpecializationDto>> Get(int id)
        {
            var userSpecialization = await _unitOfWork.UserSpessializationRepository.GetByIdAsync(id);
            if (userSpecialization == null)
            {
                return NotFound($"UserSpecialization with id {id} was not found.");
            }
            return _mapper.Map<UserSpecializationDto>(userSpecialization);
        }

        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // public async Task<ActionResult<UserSpecialization>> Post(UserSpecializationDto userSpecializationDto)
        // {
        //     var userSpecialization = _mapper.Map<UserSpecialization>(userSpecializationDto);
        //     _unitOfWork.UserSpessializationRepository.Add(userSpecialization);
        //     await _unitOfWork.SaveAsync();
        //     if (userSpecializationDto == null)
        //     {
        //         return BadRequest();
        //     }
        //     return CreatedAtAction(nameof(Post), new { id = userSpecializationDto.Id }, userSpecialization);
        // }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] UserSpecializationDto userSpecializationDto)
        {
            if (userSpecializationDto == null)
                return NotFound();

            var userSpecialization = _mapper.Map<UserSpecialization>(userSpecializationDto);
            _unitOfWork.UserSpessializationRepository.Update(userSpecialization);
            await _unitOfWork.SaveAsync();
            return Ok(userSpecializationDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var userSpecialization = await _unitOfWork.UserSpessializationRepository.GetByIdAsync(id);
            if (userSpecialization == null)
                return NotFound();

            _unitOfWork.UserSpessializationRepository.Remove(userSpecialization);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}