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
    [Route("data")]
    public class DataController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public DataController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> Listar1()
        {
            var apiUrl = "http://ip172-18-0-72-cl73oossnmng008581eg-5000.direct.labs.play-with-docker.com/api/valor";
            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserializa directamente como un diccionario anidado
                    var userRatings = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, double>>>(content);

                    // Puedes acceder a los datos de la siguiente manera
                    foreach (var (artist, ratings) in userRatings)
                    {
                        Console.WriteLine($"Artist: {artist}");
                        foreach (var (user, rating) in ratings)
                        {
                            Console.WriteLine($"User: {user}, Rating: {rating}");
                        }
                        Console.WriteLine();
                    }

                    // Devuelve los datos como un objeto JSON
                    return Ok(userRatings);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error al obtener datos de la API externa.");
                }
            }
            catch (HttpRequestException)
            {
                return StatusCode(500, "Error al conectar con la API externa.");
            }
        }
    }
}
