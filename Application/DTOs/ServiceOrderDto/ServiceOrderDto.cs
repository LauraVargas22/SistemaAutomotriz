using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.ServiceOrderDto
{
    public class ServiceOrderDto
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
    }
}