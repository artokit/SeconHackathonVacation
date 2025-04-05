using Common;
using Contracts.Employees.Responses;
using Contracts.Users.Requests;
using DataAccess.Models;

namespace Services.Mappers;

public static class UserMappers
{
    public static DbUser MapToDb(this RegisterRequestDto requestDto)
    {
        return new DbUser
        {
            Name = requestDto.Name,
            Surname = requestDto.Surname,
            Patronymic = requestDto.Patronymic,
            HashedPassword = requestDto.Password.Hash(),
            Email = requestDto.Email
        };
    }

    public static GetEmployeeResponseDto MapToDto(this DbUser dbUser)
    {
        return new GetEmployeeResponseDto
        {
            Id = dbUser.Id,
            Name = dbUser.Name,
            Surname = dbUser.Surname,
            Patronymic = dbUser.Patronymic,
            DepartmentId = dbUser.DepartmentId,
            Phone = dbUser.Phone,
            TelegramUsername = dbUser.TelegramUsername,
            Email = dbUser.Email,
            ImageId = dbUser.ImageId,
            Role = dbUser.Role
        };
    }
}