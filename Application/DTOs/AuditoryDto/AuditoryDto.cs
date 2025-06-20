using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AuditoryDto
    {
        public int Id { get; set; }
        public string? Entity { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}