using System;
using System.Threading.Tasks;
using Application.DTOs.RegisterClientWithVehicleDto;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class RegisterClientWithVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterClientWithVehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> ExecuteAsync(RegisterClientWithVehicleDto dto)
        {
            // Validación opcional: verificar que no exista otro cliente con la misma identificación
            var existingClient = await _unitOfWork.ClientRepository
                .GetByIdentificationAsync(dto.Identification!);

            if (existingClient is not null)
                throw new Exception("Ya existe un cliente con esta identificación.");

            var client = new Client
            {
                Name = dto.Name!,
                LastName = dto.LastName!,
                Email = dto.Email!,
                Phone = dto.Phone!,
                Birth = dto.Birth,
                Identification = dto.Identification!
            };

            _unitOfWork.ClientRepository.Add(client);
            await _unitOfWork.SaveAsync();

            foreach (var v in dto.Vehicles)
            {
                // Validar que el tipo de vehículo existe
                var typeVehicle = await _unitOfWork.TypeVehicleRepository.GetByIdAsync(v.TypeVehicleId);
                if (typeVehicle == null)
                    throw new Exception($"Tipo de vehículo con ID {v.TypeVehicleId} no encontrado.");

                var vehicle = new Vehicle
                {
                    Brand = v.Brand!,
                    Model = v.Model!,
                    VIN = v.VIN!,
                    Mileage = v.Mileage,
                    ClientId = client.Id,
                    TypeVehicleId = v.TypeVehicleId
                };

                _unitOfWork.VehicleRepository.Add(vehicle);
            }

            await _unitOfWork.SaveAsync();

            return client.Id;
        }
    }
}
