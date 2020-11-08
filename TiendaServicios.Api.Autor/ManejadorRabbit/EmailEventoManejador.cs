using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Interface;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Modelo;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        private readonly ILogger<EmailEventoManejador> _logger;
        private readonly ISendGridEnviar _sendGridEnviar;
        private readonly IConfiguration _configuration;

        public EmailEventoManejador(ILogger<EmailEventoManejador> logger, ISendGridEnviar sendGridEnviar, IConfiguration configuration)
        {
            _logger = logger;
            _sendGridEnviar = sendGridEnviar;
            _configuration = configuration;
        }

        public async Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation(@event.Titulo);
            var result = await _sendGridEnviar.EnviarEmail(new SendGridData
            {
                Contenido = @event.Contenido,
                EmailDestinatario = @event.Destinatario,
                NombreDestinatario = @event.Destinatario,
                Titulo = @event.Titulo,
                SendGridAPIKey = _configuration["SendGrid:ApiKey"]
            });

            if(result.resultado)
            {
                await Task.CompletedTask;
                return;
            }            
        }
    }
}
