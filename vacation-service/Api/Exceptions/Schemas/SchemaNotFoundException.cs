using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Schemas
{
    public class SchemaNotFoundException : BadRequestException
    {
        public SchemaNotFoundException(string? message = "Схема не найдена.") : base(message) { }
    }
}
