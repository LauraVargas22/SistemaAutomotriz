using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public int Id { get; set; }
        public int ServiceOrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly Date { get; set; }
        public string? Code { get; set; }

        public ServiceOrder? ServiceOrders { get; set; }
    }
}