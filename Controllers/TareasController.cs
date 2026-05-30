using Microsoft.AspNetCore.Mvc;
using Pc3_Cabana.Models;

namespace Pc3_Cabana.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TareasController : ControllerBase
{
    private static readonly List<Tarea> _tareas = [];
    private static int _nextId = 1;
    private static readonly Lock _lock = new();

    // GET /api/tareas
    [HttpGet]
    public ActionResult<IEnumerable<Tarea>> GetAll(
        [FromQuery] string? estado,
        [FromQuery] string? prioridad,
        [FromQuery] DateTime? fechaInicio,
        [FromQuery] DateTime? fechaFin)
    {
        if (estado is not null && !Enum.TryParse<EstadoTarea>(estado, ignoreCase: true, out _))
            return BadRequest($"Estado inválido. Valores permitidos: {string.Join(", ", Enum.GetNames<EstadoTarea>())}");

        if (prioridad is not null && !Enum.TryParse<PrioridadTarea>(prioridad, ignoreCase: true, out _))
            return BadRequest($"Prioridad inválida. Valores permitidos: {string.Join(", ", Enum.GetNames<PrioridadTarea>())}");

        if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
            return BadRequest("fechaInicio no puede ser mayor que fechaFin.");

        var resultado = _tareas.AsEnumerable();

        if (estado is not null)
        {
            var estadoEnum = Enum.Parse<EstadoTarea>(estado, ignoreCase: true);
            resultado = resultado.Where(t => t.Estado == estadoEnum);
        }

        if (prioridad is not null)
        {
            var prioridadEnum = Enum.Parse<PrioridadTarea>(prioridad, ignoreCase: true);
            resultado = resultado.Where(t => t.Prioridad == prioridadEnum);
        }

        if (fechaInicio.HasValue)
            resultado = resultado.Where(t => t.FechaVencimiento >= fechaInicio);

        if (fechaFin.HasValue)
            resultado = resultado.Where(t => t.FechaVencimiento <= fechaFin);

        return Ok(resultado.ToList());
    }

    // GET /api/tareas/{id}
    [HttpGet("{id:int}")]
    public ActionResult<Tarea> GetById(int id)
    {
        var tarea = _tareas.FirstOrDefault(t => t.Id == id);
        return tarea is null ? NotFound() : Ok(tarea);
    }

    // POST /api/tareas
    [HttpPost]
    public ActionResult<Tarea> Create([FromBody] Tarea tarea)
    {
        lock (_lock)
        {
            tarea.Id = _nextId++;
            tarea.FechaCreacion = DateTime.UtcNow;
            _tareas.Add(tarea);
        }
        return CreatedAtAction(nameof(GetById), new { id = tarea.Id }, tarea);
    }

    // PUT /api/tareas/{id}
    [HttpPut("{id:int}")]
    public ActionResult<Tarea> Update(int id, [FromBody] Tarea tarea)
    {
        lock (_lock)
        {
            var index = _tareas.FindIndex(t => t.Id == id);
            if (index < 0) return NotFound();

            tarea.Id = id;
            tarea.FechaCreacion = _tareas[index].FechaCreacion;
            _tareas[index] = tarea;
            return Ok(tarea);
        }
    }

    // DELETE /api/tareas/{id}
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        lock (_lock)
        {
            var tarea = _tareas.FirstOrDefault(t => t.Id == id);
            if (tarea is null) return NotFound();
            _tareas.Remove(tarea);
        }
        return NoContent();
    }
}
