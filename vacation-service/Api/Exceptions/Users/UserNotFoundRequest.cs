using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class UserNotFoundRequest : NotFoundRequestException
{
    public UserNotFoundRequest(string? message = "Пользователь не найден") : base(message) { }
}