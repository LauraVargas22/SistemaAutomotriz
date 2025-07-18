using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int ServiceOrder_Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly Date { get; set; }
        public string? Code { get; set; }
    }
}