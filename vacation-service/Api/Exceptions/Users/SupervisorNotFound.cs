using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Users;

public class SupervisorNotFound : NotFoundException
{
    public SupervisorNotFound(string? message = "Руководитель с данным id не найден") : base(message){ }
}