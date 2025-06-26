using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ApiSGTA.Controllers;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs;
using AutoMapper;

namespace ApiSGTA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
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

        [HttpGet("{userId:int}/{rolId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserRolDto>> Get(int userId, int rolId)
        {
            var userRol = await _unitOfWork.UserRolRepository.GetByIdsAsync(userId, rolId);
            if (userRol == null)
            {
                return NotFound($"UserRol with keys ({userId}, {rolId}) not found.");
            }
            return Ok(_mapper.Map<UserRolDto>(userRol));
        }

        [HttpPut("{userId:int}/{rolId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int userId, int rolId, [FromBody] UserRolDto userRolDto)
        {
            if (userRolDto == null || userRolDto.UserId != userId || userRolDto.RolId != rolId)
            {
                return BadRequest("Mismatched or invalid data.");
            }
            var existing = await _unitOfWork.UserRolRepository.GetByIdsAsync(userId, rolId);
            if (existing == null)
                return NotFound();
            var userMemberRole = _mapper.Map<UserRol>(userRolDto);
            _unitOfWork.UserRolRepository.Update(userMemberRole);
            await _unitOfWork.SaveAsync();
            return Ok(userRolDto);
        }

        [HttpDelete("{userId:int}/{rolId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int userId, int rolId)
        {
            var userRol = await _unitOfWork.UserRolRepository.GetByIdsAsync(userId, rolId);
            if (userRol == null)
                return NotFound();

            _unitOfWork.UserRolRepository.Remove(userRol);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}