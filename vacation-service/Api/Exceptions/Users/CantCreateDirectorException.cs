using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class CantCreateDirectorException : BadRequestException
{
    public CantCreateDirectorException(string? message = "Вы не можете создать директора компании") : base(message) { }
}