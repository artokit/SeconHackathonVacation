using Services.Exceptions.Abstractions;

namespace Services.Exceptions.Users;

public class EmailsExistException : BadRequestException
{
    public EmailsExistException(string? message = "Данный email уже занят") : base(message) { }
}