using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Departments;

public class DepartmentNotFoundRequest:NotFoundRequestException
{
    public DepartmentNotFoundRequest(string? message = "Отдел не найден") : base(message) { }
}