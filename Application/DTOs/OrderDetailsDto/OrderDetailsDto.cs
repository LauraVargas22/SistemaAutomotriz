using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.OrderDetailsDto
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public int ServiceOrder_Id { get; set; }
        public int SparePart_Id { get; set; }
        public int RequiredPieces { get; set; }
        public decimal TotalPrice { get; set; }
    }
}