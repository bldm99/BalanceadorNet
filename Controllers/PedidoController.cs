using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Api.Models;
using Microsoft.AspNetCore.Cors;



namespace Api.Controllers
{
    [ApiController]
    [Route("prueba")]

    public class PedidoController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        /*public PedidoController()
        {
            _httpClient = new HttpClient();
        }*/
        public PedidoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }


        [HttpGet]
        [Route("listar")]

        //public async Task<IActionResult> Listar()
        public async Task<IActionResult> Listar([FromQuery] string _id)

        {
            //var apiUrl0 = "https://teamapi.bladimirchipana.repl.co/pruebas"; // Nombre de nuestra API
            //var apiUrl = $"https://teamapi.bladimirchipana.repl.co/pedidos?_id={_id}"; // Nombre de nuestra API
            //var apiUrl = "https://teamapi.bladimirchipana.repl.co/pedidos?_id=64ab919927009eb117951833";

            //https://girapi.bladimirchipana.repl.co/registromacro?_id=6531d08612ec096c58717b97 
            var apiUrl = $"https://girapi.bladimirchipana.repl.co/registromacro?_id={_id}";
            try
            {
                // Realiza una solicitud GET a la API externa
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserializa los datos de la respuesta JSON a una lista de objetos Tienda
                    //var tiendas = JsonConvert.DeserializeObject<List<Tienda>>(content);
                    //var pedidosx = JsonConvert.DeserializeObject<List<Pedidos>>(content);

                    var macros = JsonConvert.DeserializeObject<List<Procesos>>(content);
                    var riesgos = new List<Mriesgos>();
                    foreach (var x in macros)
                    {
                        if (x.m_riesgos != null)
                        {
                            riesgos.AddRange(x.m_riesgos);
                        }
                    }

                    var riesgosUnicos = riesgos
                        .GroupBy(r => r.nombre)
                        .Select(grp => grp.First())
                        .ToList();

                    /*// Extrae solo los datos de Pedidos de todas las Tiendas
                    var pedidos = new List<Pedidos>();
                    foreach (var tienda in tiendas)
                    {
                        if (tienda.pedidos != null)
                        {
                            pedidos.AddRange(tienda.pedidos);
                        }
                    }

                    // Devuelve los datos de Pedidos obtenidos en la respuesta
                    return Ok(pedidos);*/
                    //return Ok(pedidosx);
                    return Ok(riesgosUnicos);


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
