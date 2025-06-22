using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ServiceOrderDto
    {
        public int Id { get; set; }
        public int VehiclesId { get; set; }
        public int TypeServiceId { get; set; }
        public int StateId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public bool IsAuthorized { get; set; }
        public string? ClientMessage { get; set; }
        public int UserId { get; set; }
    }
}