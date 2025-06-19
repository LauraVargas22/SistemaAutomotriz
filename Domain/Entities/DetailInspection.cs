using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetailInspection : BaseEntity
    {
        public int Id { get; set; }
        public int ServiceOrderId { get; set; }
        public int InspectionId { get; set; }
        public int Quantity { get; set; }

        public ServiceOrder? ServiceOrder { get; set; }
        public Inspection? Inspection { get; set; }
    }
}