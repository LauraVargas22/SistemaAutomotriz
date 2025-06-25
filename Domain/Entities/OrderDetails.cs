using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetails : BaseEntity
    {
        public int Id { get; set; }
        public int ServiceOrderId { get; set; }
        public int SparePartId { get; set; }
        public int RequiredPieces { get; set; }
        public decimal TotalPrice { get; set; }

        public SparePart? SpareParts { get; set; }
        public ServiceOrder? ServiceOrder { get; set; }
    }
}