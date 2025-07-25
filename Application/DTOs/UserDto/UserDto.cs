using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}