using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IEmailService
    {
        Task SendServiceOrderCreatedEmailAsync(string clientEmail, string clientName, int orderNumber);
        Task SendServiceOrderUpdatedEmailAsync(string clientEmail, string clientName, int orderNumber, string status);
    }
}