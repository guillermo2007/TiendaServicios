using System.Threading.Tasks;
using TiendaServicios.RabbitMQ.Bus.Commands;
using TiendaServicios.RabbitMQ.Bus.Events;

namespace TiendaServicios.RabbitMQ.Bus.BusRabbit
{
    public interface IRabbitEventBus
    {
        Task SendCommand<T>(T comando) where T : Comando;
        void Publish<T>(T evento) where T: Evento;
        void Suscribe<T, TH>() where T : Evento
                               where TH : IEventoManejador<T>;
    }
}
