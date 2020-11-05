using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.MessageHandler
{
    public class LibroHandler : DelegatingHandler
    {
        private readonly ILogger<LibroHandler> _logger;
        private readonly IAutorRemote _autorRemote;
        public LibroHandler(ILogger<LibroHandler> logger, IAutorRemote autorRemote)
        {
            _logger = logger;
            _autorRemote = autorRemote;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation)
        {
            var time = Stopwatch.StartNew();
            _logger.LogInformation("Inicio request");
            var response = await base.SendAsync(request, cancellation);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultado = JsonSerializer.Deserialize<LibroModeloRemote>(content, options);
                var responseAutor = await _autorRemote.GetAutor(resultado.AutorLibro ?? Guid.Empty);
                if(responseAutor.resultado)
                {
                    var objetoAutor = responseAutor.autor;
                    resultado.AutorData = objetoAutor;
                    var resultadoString = JsonSerializer.Serialize(resultado);
                    response.Content = new StringContent(resultadoString, System.Text.Encoding.UTF8, "application/json");
                }
            }

            _logger.LogInformation($"Proceso se hizo en {time.ElapsedMilliseconds}ms");
            return response;
        }

    }
}
