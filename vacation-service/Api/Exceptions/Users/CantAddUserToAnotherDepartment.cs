using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class CantAddUserToAnotherDepartment : ForbiddenRequestException
{
    public CantAddUserToAnotherDepartment(string? message = "Вы не можете добавить пользователя в этот отдел") : base(message) { }
}