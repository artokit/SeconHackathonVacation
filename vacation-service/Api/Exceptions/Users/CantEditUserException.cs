using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class CantEditUserException : ForbiddenRequestException
{
    public CantEditUserException(string? message = "Вы не можете редактировать данного пользователя") : base(message) { }
}