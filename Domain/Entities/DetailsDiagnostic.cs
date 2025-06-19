using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetailsDiagnostic : BaseEntity
    {
         public int ServiceOrder_Id { get; set; }
        public int Diagnostic_Id { get; set; }

        public ServiceOrder? ServiceOrder { get; set; }
        public Diagnostic? Diagnostic { get; set; }
    }
}