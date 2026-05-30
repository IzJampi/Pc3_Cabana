using System.ComponentModel.DataAnnotations;

namespace Pc3_Cabana.Models;

public enum EstadoTarea { Pendiente, EnProceso, Completada }

public enum PrioridadTarea { Baja, Media, Alta }

public class Tarea
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio.")]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [EnumDataType(typeof(EstadoTarea), ErrorMessage = "Estado inválido.")]
    public EstadoTarea Estado { get; set; }

    [Required(ErrorMessage = "La prioridad es obligatoria.")]
    [EnumDataType(typeof(PrioridadTarea), ErrorMessage = "Prioridad inválida.")]
    public PrioridadTarea Prioridad { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [FechaVencimientoValida]
    public DateTime? FechaVencimiento { get; set; }
}

public class FechaVencimientoValidaAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime fecha && fecha.Date < DateTime.Today)
            return new ValidationResult("La fecha de vencimiento no puede ser menor a la fecha actual.");
        return ValidationResult.Success;
    }
}
