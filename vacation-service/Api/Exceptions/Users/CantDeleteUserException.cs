using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class CantDeleteUserException : ForbiddenRequestException
{
    public CantDeleteUserException(string? message = "Вы не можете удалить сотрудника") : base(message) { }
}