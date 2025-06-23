using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vehicle : BaseEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? VIN { get; set; }
        public int Mileage { get; set; }
        public int TypeVehicleId { get; set; }
        public TypeVehicle? TypeVehicle { get; set; }

        public Client? Clients { get; set; }

        public ICollection<ServiceOrder> ServiceOrders { get; set; } = new HashSet<ServiceOrder>();
    }
}