using Services.Exceptions.Abstractions;

namespace Services.Exceptions.Users;

public class EmailIsExistException : BadRequestException
{
    public EmailIsExistException(string? message = "Данный email уже занят") : base(message) { }
}