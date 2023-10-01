using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("prueba")]
    public class PedidoController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PedidoController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> Listar()
        {
            var apiUrl = "https://teamapi.bladimirchipana.repl.co/pruebas"; // Nombre de nuestra API

            try
            {
                // Realiza una solicitud GET a la API externa
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserializa los datos de la respuesta JSON a una lista de objetos Tienda
                    var tiendas = JsonConvert.DeserializeObject<List<Tienda>>(content);

                    // Extrae solo los datos de Pedidos de todas las Tiendas
                    var pedidos = new List<Pedidos>();
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.pedidos != null)
                        {
                            pedidos.AddRange(tienda.pedidos);
                        }
                    }

                    // Devuelve los datos de Pedidos obtenidos en la respuesta
                    return Ok(pedidos);
                }
                else
                {
                    // Maneja el caso en que la solicitud a la API externa no sea exitosa
                    return StatusCode((int)response.StatusCode, "Error al obtener datos de la API externa.");
                }
            }
            catch (HttpRequestException)
            {
                // Maneja las excepciones de solicitud HTTP, por ejemplo, si no se puede conectar a la API externa
                return StatusCode(500, "Error al conectar con la API externa.");
            }
        }
    }
}
