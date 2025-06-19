using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetaillInspection : BaseEntity
    {
         public int Id { get; set; }
        public int ServiceOrder_Id { get; set; }
        public int Inspection_Id { get; set; }
        public int Quantity { get; set; }

        public ServiceOrder? ServiceOrder { get; set; }
        public Inspection? Inspections { get; set; }
    }
}