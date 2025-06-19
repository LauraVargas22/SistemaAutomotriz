using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserSpecialization : BaseEntity
    { 
        
        public int UserId { get; set; }
        public int SpecializationId { get; set; }

        public User? User { get; set; }
        public Specialization? Specialization { get; set; }
    }
}