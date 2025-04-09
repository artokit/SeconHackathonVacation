using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class FailureAuthorizationRequestException : NotFoundRequestException
{
    public FailureAuthorizationRequestException(string? message = "Неверный логин или пароль") : base(message) { }
}