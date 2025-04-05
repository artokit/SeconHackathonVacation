using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class UserNotFound : NotFoundException
{
    public UserNotFound(string? message = "Пользователь не найден") : base(message) { }
}