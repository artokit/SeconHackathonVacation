using System.Text.Json.Serialization;

namespace Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Roles
{
    Director,
    Hr,
    Worker
}