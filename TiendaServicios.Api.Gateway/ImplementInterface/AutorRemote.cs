using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.Gateway.InterfaceRemote;
using TiendaServicios.Api.Gateway.LibroRemote;

namespace TiendaServicios.Api.Gateway.ImplementInterface
{
    public class AutorRemote : IAutorRemote
    {
        private readonly ILogger<AutorRemote> _logger;
        private readonly IHttpClientFactory _httpClient;
        public AutorRemote(ILogger<AutorRemote> logger, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
        public async Task<(bool resultado, AutorModeloRemote autor, string Error)> GetAutor(Guid autorId)
        {
            try
            {
                var client = _httpClient.CreateClient("AutorService");
                var response = await client.GetAsync($"/Autor/{autorId}");
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<AutorModeloRemote>(content, options);
                    return (true, resultado, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
