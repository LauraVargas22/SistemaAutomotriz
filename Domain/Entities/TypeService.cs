using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TypeService : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }

        public ICollection<ServiceOrder> ServiceOrders { get; set; } = new HashSet<ServiceOrder>();
    }
}