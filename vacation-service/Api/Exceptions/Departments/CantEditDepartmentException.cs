using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Departments;

public class CantEditDepartmentException : ForbiddenRequestException
{
    public CantEditDepartmentException(string? message = "Вы не можете менять отдел") : base(message) { }
}