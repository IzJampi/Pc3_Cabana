using Microsoft.AspNetCore.Mvc;
using Pc3_Cabana.Models;
using System.Net.Http.Json;

namespace Pc3_Cabana.Controllers;

[ApiController]
[Route("api/tareas-externas")]
public class TareasExternasController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string ApiUrl = "https://jsonplaceholder.typicode.com/todos";

    public TareasExternasController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET /api/tareas-externas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaExternaDto>>> GetAll()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var todos = await client.GetFromJsonAsync<List<JsonPlaceholderTodo>>(ApiUrl);

            if (todos is null) return StatusCode(502, "La API externa no devolvió datos.");

            return Ok(todos.Select(Mapear));
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "No se pudo conectar con la API externa.");
        }
    }

    // GET /api/tareas-externas/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TareaExternaDto>> GetById(int id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{ApiUrl}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound($"No existe una tarea externa con ID {id}.");

            response.EnsureSuccessStatusCode();

            var todo = await response.Content.ReadFromJsonAsync<JsonPlaceholderTodo>();
            if (todo is null) return NotFound($"No existe una tarea externa con ID {id}.");

            return Ok(Mapear(todo));
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, "No se pudo conectar con la API externa.");
        }
    }

    private static TareaExternaDto Mapear(JsonPlaceholderTodo todo) => new()
    {
        ExternalId = todo.Id,
        Titulo = todo.Title,
        Completado = todo.Completed
    };
}
