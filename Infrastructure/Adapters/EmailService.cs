using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Ports;
using Infrastructure.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;

namespace Infrastructure.Adapters
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration configuration)
        {
            _emailConfiguration = configuration;
        }

        public async Task SendServiceOrderCreatedEmailAsync(string clientEmail, string clientName, int orderNumber)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfiguration.SenderName, _emailConfiguration.SenderEmail));
            message.To.Add(new MailboxAddress(clientName, clientEmail));
            message.Subject = $"New Service Order - {orderNumber}";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = GenerateServiceOrderEmailTemplate(clientName, orderNumber)
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            // Use StartTls for port 587, SslOnConnect for port 465
            var secureOption = _emailConfiguration.SmtpPort == 587
                ? SecureSocketOptions.StartTls
                : SecureSocketOptions.SslOnConnect;

            await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, secureOption);
            await client.AuthenticateAsync(_emailConfiguration.SenderEmail, _emailConfiguration.SenderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendServiceOrderUpdatedEmailAsync(string clientEmail, string clientName, int orderNumber, string status)
        {
            // TODO: Implement email logic for updated service order
            await Task.CompletedTask;
        }

        private string GenerateServiceOrderEmailTemplate(string customerName, int orderNumber)
        {
            return $@"
                <html>
                <body>
                    <h2>¡Orden de Servicio Creada!</h2>
                    <p>Estimado/a <strong>{customerName}</strong>,</p>
                    <p>Su orden de servicio <strong>#{orderNumber}</strong> ha sido creada exitosamente.</p>
                    <p>Por favor ingrese a nuestra página web y acepte los términos y condiciones para coordinar el servicio de su vehículo.</p>
                    <br/>
                    <p>Atentamente,<br/>
                    <strong>Equipo del Taller Automotriz</strong></p>
                </body>
                </html>";
        }
    }
}