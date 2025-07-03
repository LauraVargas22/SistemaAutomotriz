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
                var existingClient = await _unitOfWork.ClientRepository
                    .GetByIdentificationAsync(dto.Identification!);

                if (existingClient is not null)
                    throw new Exception("Ya existe un cliente con esta identificación.");

                var client = new Client
                {
                    Name = dto.Name!,
                    LastName = dto.LastName!,
                    Email = dto.Email!,
                    Birth = dto.Birth,
                    Identification = dto.Identification!
                };

                _unitOfWork.ClientRepository.Add(client);
                await _unitOfWork.SaveAsync(); // ⬅️ Aquí se guarda el cliente para obtener su Id

                // ✅ Aquí va el bloque B: registrar teléfonos si existen
                if (dto.TelephoneNumbers != null && dto.TelephoneNumbers.Any())
                {
                    foreach (var phone in dto.TelephoneNumbers)
                    {
                        var telephone = new TelephoneNumbers
                        {
                            ClientId = client.Id,
                            Number = phone
                        };

                        _unitOfWork.TelephoneNumbersRepository.Add(telephone);
                    }

                    await _unitOfWork.SaveAsync(); // ⬅️ Guardar los teléfonos
                }

                // 🚗 Luego se registran los vehículos
                if (dto.Vehicles == null || !dto.Vehicles.Any())
                {
                    throw new Exception("Debe registrar al menos un vehículo.");
                }

                foreach (var v in dto.Vehicles)
                {
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

                await _unitOfWork.SaveAsync(); // ⬅️ Guardar vehículos

                return client.Id;
            }

    }
}
