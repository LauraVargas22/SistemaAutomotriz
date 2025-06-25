using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTOs
{
    public class TypeServiceDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
    }
}