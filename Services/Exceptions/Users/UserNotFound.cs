using Services.Exceptions.Abstractions;

namespace Services.Exceptions.Users;

public class UserNotFound : NotFoundException
{
    public UserNotFound(string? message = "Пользователь не найден") : base(message) { }
}