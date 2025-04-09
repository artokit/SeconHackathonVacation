using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class SupervisorNotFoundRequest : NotFoundRequestException
{
    public SupervisorNotFoundRequest(string? message = "Руководитель с данным id не найден") : base(message){ }
}