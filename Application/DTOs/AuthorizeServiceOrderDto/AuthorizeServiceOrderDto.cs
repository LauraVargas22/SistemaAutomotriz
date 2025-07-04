using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AuthorizeServiceOrderDto
    {
        public int ServiceOrderId { get; set; }
        public bool IsAuthorized { get; set; }
        public string? ClientMessage { get; set; }
    }
} 