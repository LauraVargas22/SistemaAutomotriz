using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ServiceOrder : BaseEntity
    {
        public int Id { get; set; }
        public int VehiclesId { get; set; }
        public int TypeServiceId { get; set; }
        public int StateId { get; set; }
        public DateOnly? EntryDate { get; set; }
        public DateOnly? ExitDate { get; set; }
        public bool IsAuthorized { get; set; }
        public string? ClientMessage { get; set; }
        public int UserId { get; set; }
        public User? Users { get; set; }

        public State? State { get; set; }
        public TypeService? TypeService { get; set; }
        public Vehicle? Vehicle { get; set; }
        public ICollection<DetailInspection> DetaillInspections { get; set; } = new HashSet<DetailInspection>();
        public ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet<OrderDetails>();

        public ICollection<DetailsDiagnostic> DetailsDiagnostics { get; set; } = new HashSet<DetailsDiagnostic>();
        public Invoice? Invoices { get; set; }
    }
}