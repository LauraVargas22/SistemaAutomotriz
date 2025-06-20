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
    public class UserController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with id {id} was not found.");
            }
            return _mapper.Map<UserDto>(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Post(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveAsync();
            if (userDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = userDto.Id }, user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
                return NotFound();

            var user = _mapper.Map<User>(userDto);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            _unitOfWork.UserRepository.Remove(user);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}