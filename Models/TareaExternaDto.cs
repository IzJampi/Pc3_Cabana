using System.Text.Json.Serialization;

namespace Pc3_Cabana.Models;

public class TareaExternaDto
{
    public int ExternalId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public bool Completado { get; set; }
}

// Modelo interno para deserializar la respuesta de jsonplaceholder
public class JsonPlaceholderTodo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("completed")]
    public bool Completed { get; set; }
}
