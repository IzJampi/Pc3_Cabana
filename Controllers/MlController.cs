using Microsoft.AspNetCore.Mvc;
using Pc3_Cabana.MLModel;
using System.ComponentModel.DataAnnotations;

namespace Pc3_Cabana.Controllers;

[ApiController]
[Route("api/ml")]
public class MlController : ControllerBase
{
    private readonly SentimientoService _sentimientoService;

    public MlController(SentimientoService sentimientoService)
    {
        _sentimientoService = sentimientoService;
    }

    // POST /api/ml/sentimiento
    [HttpPost("sentimiento")]
    public ActionResult Analizar([FromBody] SentimientoRequest request)
    {
        try
        {
            var sentimiento = _sentimientoService.Predecir(request.Comentario);
            return Ok(new { comentario = request.Comentario, sentimiento });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al procesar el modelo: {ex.Message}");
        }
    }
}

public class SentimientoRequest
{
    [Required(ErrorMessage = "El comentario es obligatorio.")]
    public string Comentario { get; set; } = string.Empty;
}
