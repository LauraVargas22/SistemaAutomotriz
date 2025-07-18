using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateOnly Birth { get; set; }
        public string? Identification { get; set; }
        public List<TelephoneNumbersDto>? TelephoneNumbers { get; set; }  
    }
}