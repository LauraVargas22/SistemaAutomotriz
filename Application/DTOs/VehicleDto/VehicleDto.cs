using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? VIN { get; set; }
        public int Mileage { get; set; }
        public int TypeVehicle { get; set; }       
    }
}