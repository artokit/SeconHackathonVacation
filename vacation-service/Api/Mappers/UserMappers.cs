using Api.Dto.Authorization.Requests;
using Api.Dto.Users.Requests;
using Api.Dto.Users.Responses;
using Common;
using DataAccess.Models;

namespace Api.Mappers;

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

    public static GetUserResponseDto MapToDto(this DbUser dbUser)
    {
        return new GetUserResponseDto
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
    
    public static DbUser MapToDb(this CreateUserRequestDto request, string generatedPassword)
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