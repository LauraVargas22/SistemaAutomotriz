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
            var userSpecializations = await _unitOfWork.UserSpecializationRepository.GetAllAsync();
            return _mapper.Map<List<UserSpecializationDto>>(userSpecializations);
        }

        [HttpGet("{userId:int}/{specializationId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserSpecializationDto>> Get(int userId, int specializationId)
        {
            var userSpecialization = await _unitOfWork.UserSpecializationRepository.GetByIdsAsync(userId, specializationId);
            if (userSpecialization == null)
            {
                return NotFound($"User Specialization with keys ({userId}, {specializationId}) not found.");
            }
            return Ok(_mapper.Map<UserRolDto>(userSpecialization));
        }

        [HttpPut("{userId:int}/{specializationId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int userId, int specializationId, [FromBody] UserSpecializationDto userSpecializationDto)
        {
            if (userSpecializationDto == null || userSpecializationDto.UserId != userId || userSpecializationDto.SpecializationId != specializationId)
            {
                return BadRequest("Mismatched or invalid data.");
            }
            var existing = await _unitOfWork.UserSpecializationRepository.GetByIdsAsync(userId, specializationId);
            if (existing == null)
                return NotFound();
            var userSpecialization = _mapper.Map<UserSpecialization>(userSpecializationDto);
            _unitOfWork.UserSpecializationRepository.Update(userSpecialization);
            await _unitOfWork.SaveAsync();
            return Ok(userSpecializationDto);
        }

        [HttpDelete("{userId:int}/{specializationId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int userId, int specializationId)
        {
            var userSpecialization = await _unitOfWork.UserSpecializationRepository.GetByIdsAsync(userId, specializationId);
            if (userSpecialization == null)
                return NotFound();

            _unitOfWork.UserSpecializationRepository.Remove(userSpecialization);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}