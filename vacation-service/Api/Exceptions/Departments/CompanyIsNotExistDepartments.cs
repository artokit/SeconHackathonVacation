using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Departments;

public class CompanyIsNotExistDepartments  : NotFoundRequestException
{
    public CompanyIsNotExistDepartments(string? message = "Компания не содержит ни одного отдела.") : base(message){ }
}