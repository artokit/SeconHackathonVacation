using System.Text.Json.Serialization;

namespace Application.FileService.Models;

public class Image
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("extension")]
    public string Extension { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }
}