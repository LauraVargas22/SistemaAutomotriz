using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.RegisterClientWithVehicleDto
{
    public class RegisterClientWithVehicleDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime Birth { get; set; }
        public string? Identification { get; set; }

        // Lista de vehículos que se van a registrar para ese cliente
        public List<RegisterVehicleDto> Vehicles { get; set; } = new();
    }

    public class RegisterVehicleDto
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? VIN { get; set; }
        public int Mileage { get; set; }
        public int TypeVehicleId { get; set; }
        
    }
}