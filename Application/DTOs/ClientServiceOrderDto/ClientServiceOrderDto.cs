using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClientServiceOrderDto
    {
        public int Id { get; set; }
        public int VehiclesId { get; set; }
        public int TypeServiceId { get; set; }
        public int StateId { get; set; }
        public DateOnly EntryDate { get; set; }
        public DateOnly ExitDate { get; set; }
        public bool IsAuthorized { get; set; }
        public string? ClientMessage { get; set; }
        public int UserId { get; set; }
        
        // Información del vehículo
        public string? VehicleBrand { get; set; }
        public string? VehicleModel { get; set; }
        public string? VehicleVIN { get; set; }
        
        // Información del tipo de servicio
        public string? TypeServiceName { get; set; }
        public decimal TypeServicePrice { get; set; }
        public int TypeServiceDuration { get; set; }
        
        // Información del estado
        public string? StateName { get; set; }
        
        // Información del usuario (mecánico)
        public string? UserName { get; set; }
    }
} 