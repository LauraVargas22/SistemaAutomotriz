using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class TelephoneNumbers : BaseEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [JsonIgnore]
        public Client? Client { get; set; }
        public string? Number { get; set; }
    }
}