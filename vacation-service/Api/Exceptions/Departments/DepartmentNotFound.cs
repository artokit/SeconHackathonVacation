using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Departments;

public class DepartmentNotFound:NotFoundException
{
    public DepartmentNotFound(string? message = "Отдел не найден") : base(message) { }
}