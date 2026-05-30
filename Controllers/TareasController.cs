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
    public ActionResult<IEnumerable<Tarea>> GetAll() => Ok(_tareas);

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
