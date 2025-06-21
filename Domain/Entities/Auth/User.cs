using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public ICollection<ServiceOrder>? ServiceOrders { get; set; } = new HashSet<ServiceOrder>();
        public ICollection<Auditory>? Auditories { get; set; } = new HashSet<Auditory>();
        public ICollection<Rol>? Rols { get; set; } = new HashSet<Rol>();
        public ICollection<Diagnostic>? Diagnostics { get; set; } = new HashSet<Diagnostic>();
        public ICollection<UserSpecialization>? UserSpecializations { get; set; } = new HashSet<UserSpecialization>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
        public ICollection<UserRol>? UserRols { get; set; }
    }
}
