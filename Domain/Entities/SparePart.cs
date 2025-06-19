using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SparePart : BaseEntity
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int MiniStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Category { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet <OrderDetails> ();
    }
}