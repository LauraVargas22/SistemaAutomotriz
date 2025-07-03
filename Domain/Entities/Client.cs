using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateOnly Birth { get; set; }
        public string? Identification { get; set; }

        public ICollection<Vehicle>? Vehicles { get; set; } = new HashSet<Vehicle>();
        public ICollection<ServiceOrder>? ServiceOrders { get; set; } = new HashSet<ServiceOrder>();
        public ICollection<TelephoneNumbers>? TelephoneNumbers { get; set; } = new HashSet<TelephoneNumbers>();
    } 
} 