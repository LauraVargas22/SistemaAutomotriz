using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs.DetailInspectionDto
{
    public class DetailInspectionDto
    {
        public int Id { get; set; }
        public int ServiceOrder_Id { get; set; }
        public int Inspection_Id { get; set; }
        public int Quantity { get; set; }
    }
}