using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ApiSGTA.Controllers;
using Application.DTOs;
using AutoMapper;
using Application.Services;
using Application.DTOs.RegisterClientWithVehicleDto;

namespace ApiSGTA.Controllers
{
    public class ClientController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RegisterClientWithVehicleService _registerClientWithVehicleService;


        public ClientController(IUnitOfWork unitOfWork, IMapper mapper, RegisterClientWithVehicleService registerClientWithVehicleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> Get()
        {
            var clients = await _unitOfWork.ClientRepository.GetAllAsync();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientDto>> Get(int id)
        {
            var clients = await _unitOfWork.ClientRepository.GetByIdAsync(id);
            if (clients == null)
            {
                return NotFound($"Clients with id {id} was not found.");
            }
            return _mapper.Map<ClientDto>(clients);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> Post(ClientDto clientDto)
        {
            var clients = _mapper.Map<Client>(clientDto);
            _unitOfWork.ClientRepository.Add(clients);
            await _unitOfWork.SaveAsync();
            if (clientDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(Post), new { id = clientDto.Id }, clients);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] ClientDto clientDto)
        {
            // Validación: objeto nulo
            if (clientDto == null)
                return NotFound();

            var hasActiveOrders = await _unitOfWork.ServiceOrderRepository.GetActiveOrdersByClientIdAsync(id);

            if (hasActiveOrders.Any())
            {
                return Conflict("No se puede editar el cliente porque tiene órdenes de servicio activas.");
            }

            var clients = _mapper.Map<Client>(clientDto);
            _unitOfWork.ClientRepository.Update(clients);
            await _unitOfWork.SaveAsync();
            return Ok(clientDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _unitOfWork.ClientRepository.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            var hasActiveOrders = await _unitOfWork.ServiceOrderRepository.GetActiveOrdersByClientIdAsync(id);

            if (hasActiveOrders.Any())
            {
                return Conflict("No se puede eliminar el cliente porque tiene órdenes de servicio activas.");
            }

            _unitOfWork.ClientRepository.Remove(client);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpPost("register-with-vehicles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> RegisterClientWithVehicles([FromBody] RegisterClientWithVehicleDto dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            try
            {
                var clientId = await _registerClientWithVehicleService.ExecuteAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = clientId }, new { message = "Cliente creado con éxito", clientId });
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

            }
}