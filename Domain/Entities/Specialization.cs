using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Specialization : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<UserSpecialization>? UserSpecialization { get; set; } = new HashSet<UserSpecialization>();
    }
}