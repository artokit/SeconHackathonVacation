using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class FailureAuthorizationException : NotFoundException
{
    public FailureAuthorizationException(string? message = "Неверный логин или пароль") : base(message) { }
}