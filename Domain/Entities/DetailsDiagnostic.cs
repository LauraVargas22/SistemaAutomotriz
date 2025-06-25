using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetailsDiagnostic
    {
         public int ServiceOrderId { get; set; }
        public int DiagnosticId { get; set; }

        public ServiceOrder? ServiceOrder { get; set; }
        public Diagnostic? Diagnostic { get; set; }
    }
}