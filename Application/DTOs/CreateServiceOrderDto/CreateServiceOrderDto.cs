using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.DTOs.CreateServiceOrderDto
{
    public class CreateServiceOrderDto
    {
        public int Vehicle_id { get; set; }
        public int Type_service_id { get; set; }
        public int Client_id { get; set; }
        public int State_id { get; set; }
        public DateTime Entry_date { get; set; }
        public string? Client_message { get; set; } 
        public List<OrderDetailDto> order_details { get; set; } = new();
        } 
        public class OrderDetailDto
        {
            public int space_part_id { get; set; }
            public int required_pieces { get; set; }
        }
    }
