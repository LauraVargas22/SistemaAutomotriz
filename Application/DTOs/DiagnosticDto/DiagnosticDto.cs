using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DiagnosticDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Description { get; set; }
        public DateOnly Date { get; set; }
    }
}