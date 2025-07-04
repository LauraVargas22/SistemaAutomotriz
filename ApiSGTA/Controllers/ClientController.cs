using System;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ApiSGTA.Controllers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Application.Services;
using Application.DTOs.RegisterClientWithVehicleDto;
using ApiSGTA.Helpers;

namespace ApiSGTA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, Recepcionist")]
    public class ClientController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RegisterClientWithVehicleService _registerClientWithVehicleService;


        public ClientController(IUnitOfWork unitOfWork, IMapper mapper, RegisterClientWithVehicleService registerClientWithVehicleService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _registerClientWithVehicleService = registerClientWithVehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> Get()
        {
            var clients = await _unitOfWork.ClientRepository.GetAllAsync();
            return Ok(_mapper.Map<List<ClientDto>>(clients));
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientDto>> Post([FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
                return BadRequest();

            // 1) Mapeo DTO -> entidad (sin ciclos)
            var client = _mapper.Map<Client>(clientDto);

            // 2) Guardo el cliente para que se genere el Id
            _unitOfWork.ClientRepository.Add(client);
            await _unitOfWork.SaveAsync();

            // 3) Guardo los teléfonos si los hay
            if (clientDto.TelephoneNumbers?.Any() == true)
            {
                foreach (var telDto in clientDto.TelephoneNumbers)
                {
                    _unitOfWork.TelephoneNumbersRepository.Add(new TelephoneNumbers
                    {
                        ClientId = client.Id,
                        Number   = telDto.Number
                    });
                }
                await _unitOfWork.SaveAsync();
            }

            // 4) Recargo el cliente completo con sus teléfonos
            var created = await _unitOfWork.ClientRepository.GetByIdAsync(client.Id);

            // 5) Mapeo entidad -> DTO (ya con Id y teléfonos)
            var resultDto = _mapper.Map<ClientDto>(created);

            // 6) Devuelvo CreatedAt con el DTO
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
                return BadRequest("Cliente inválido.");

            var client = await _unitOfWork.ClientRepository.GetByIdAsync(id); // Incluye TelephoneNumbers por el Include del repo
            if (client == null)
                return NotFound($"Cliente con id {id} no encontrado.");

            var hasActiveOrders = await _unitOfWork.ServiceOrderRepository.GetActiveOrdersByClientIdAsync(id);
            if (hasActiveOrders.Any())
            {
                return Conflict("No se puede editar el cliente porque tiene órdenes de servicio activas.");
            }

            // Actualiza datos del cliente
            client.Name = clientDto.Name;
            client.LastName = clientDto.LastName;
            client.Email = clientDto.Email;
            client.Birth = clientDto.Birth;
            client.Identification = clientDto.Identification;

            // Sincronizar teléfonos
            var incomingPhones = clientDto.TelephoneNumbers ?? new List<TelephoneNumbersDto>();
            var currentPhones = client.TelephoneNumbers ?? new List<TelephoneNumbers>();

            // Eliminar los que ya no están en el DTO
            var toRemove = currentPhones
                .Where(db => !incomingPhones.Any(i => i.Id == db.Id))
                .ToList();

            foreach (var phone in toRemove)
                _unitOfWork.TelephoneNumbersRepository.Remove(phone);

            // Agregar nuevos o actualizar existentes
            foreach (var dtoPhone in incomingPhones)
            {
                var existing = currentPhones.FirstOrDefault(p => p.Id == dtoPhone.Id);
                if (existing != null)
                {
                    // Actualizar número existente
                    existing.Number = dtoPhone.Number;
                }
                else
                {
                    // Agregar nuevo teléfono
                    _unitOfWork.TelephoneNumbersRepository.Add(new TelephoneNumbers
                    {
                        ClientId = client.Id,
                        Number = dtoPhone.Number
                    });
                }
            }

            await _unitOfWork.SaveAsync();

            return Ok("Cliente actualizado correctamente.");
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

        [HttpGet("paginated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "")
        {
            var (totalRegisters, registers) = await _unitOfWork.ClientRepository.GetAllAsync(pageNumber, pageSize, search);
            var clientDtos = _mapper.Map<List<ClientDto>>(registers);
            
            // Agregar X-Total-Count en los encabezados HTTP
            Response.Headers.Append("X-Total-Count", totalRegisters.ToString());
            
            return Ok(clientDtos);
        }
    }
}