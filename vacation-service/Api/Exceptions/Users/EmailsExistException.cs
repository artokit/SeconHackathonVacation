using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class EmailIsExistException : BadRequestException
{
    public EmailIsExistException(string? message = "Данный email уже занят") : base(message) { }
}