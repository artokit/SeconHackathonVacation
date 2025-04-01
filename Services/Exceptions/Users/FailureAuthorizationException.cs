using Services.Exceptions.Abstractions;

namespace Services.Exceptions.Users;

public class FailureAuthorizationException : NotFoundException
{
    public FailureAuthorizationException(string? message = "Неверный логин или пароль") : base(message) { }
}