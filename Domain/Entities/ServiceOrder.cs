using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ServiceOrder : BaseEntity
    {
        public int Id { get; set; }
        public int Vehicles_Id { get; set; }
        public int TypeService_Id { get; set; }
        public int Client_Id { get; set; }
        public int State_Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public bool IsAuthorized { get; set; }
        public string? ClientMessage { get; set; }
        public int UserId { get; set; }
        public User? Users { get; set; }

        public State? State { get; set; }
        public TypeService? TypeService { get; set; }
        public Vehicle? Vehicle { get; set; }
        public Client? Client { get; set; }
        public ICollection<DetaillInspection> DetaillInspections { get; set; } = new HashSet<DetaillInspection>();
        public ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet<OrderDetails>();

        public ICollection<DetailsDiagnostic> DetailsDiagnostics { get; set; } = new HashSet<DetailsDiagnostic>();
        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}