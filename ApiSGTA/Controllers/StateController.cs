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
    
    public class StateController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StateController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        
        [HttpGet]
        [Authorize(Roles = "Receptionist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<StateDto>>> Get()
        {
            var states = await _unitOfWork.StateRepository.GetAllAsync();
            return _mapper.Map<List<StateDto>>(states);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StateDto>> Get(int id)
        {
            var state = await _unitOfWork.StateRepository.GetByIdAsync(id);
            if (state == null)
            {
                return NotFound($"State with id {id} was not found.");
            }
            return _mapper.Map<StateDto>(state);
        }



        [HttpPost]
        [Authorize(Roles = "Receptionist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<State>> Post(StateDto stateDto)
        {
            var state = _mapper.Map<State>(stateDto);
            _unitOfWork.StateRepository.Add(state);
            await _unitOfWork.SaveAsync();
            if (stateDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = stateDto.Id }, state);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] StateDto stateDto)
        {
            if (stateDto == null)
                return NotFound();

            var state = _mapper.Map<State>(stateDto);
            _unitOfWork.StateRepository.Update(state);
            await _unitOfWork.SaveAsync();
            return Ok(stateDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var state = await _unitOfWork.StateRepository.GetByIdAsync(id);
            if (state == null)
                return NotFound();

            _unitOfWork.StateRepository.Remove(state);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}