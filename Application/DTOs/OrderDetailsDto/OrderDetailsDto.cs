using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public int ServiceOrderId { get; set; }
        public int SparePartId { get; set; }
        public int RequiredPieces { get; set; }
        public decimal TotalPrice { get; set; }
    }
}