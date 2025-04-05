using Api.Dto.Employees.Requests;
using Common;
using DataAccess.Models;

namespace Api.Mappers;

public static class EmployeesMappers
{
    public static DbUser MapToDb(this CreateEmployeeRequestDto request, string generatedPassword)
    {
        return new DbUser
        {
            Name = request.Name,
            Surname = request.Surname,
            Patronymic = request.Patronymic,
            HashedPassword = generatedPassword.Hash(),
            Email = request.Email,
            Role = request.Role
        };
    }
}