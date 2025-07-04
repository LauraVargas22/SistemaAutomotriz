using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs;
using AutoMapper;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace ApiSGTA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelephoneNumbersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TelephoneNumbersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelephoneNumbersDto>>> Get()
        {
            var phones = await _unitOfWork.TelephoneNumbersRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TelephoneNumbersDto>>(phones));
        }

        [HttpPost]
        public async Task<ActionResult<TelephoneNumbersDto>> Post(TelephoneNumbersDto dto)
        {
            var phone = _mapper.Map<TelephoneNumbers>(dto);
            _unitOfWork.TelephoneNumbersRepository.Add(phone);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Post), new { id = phone.Id }, phone);
        }

        // Agrega PUT, DELETE si los necesitas
    }

}