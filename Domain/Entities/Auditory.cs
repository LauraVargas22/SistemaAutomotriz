using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Auditory : BaseEntity
    {
        public int Id { get; set; }
        public string? Entity { get; set; }
        public DateTime Date { get; set; }
        public enum TypeAction
        {
            Create,
            Update,
            Delete
        }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}