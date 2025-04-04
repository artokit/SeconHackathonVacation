using Common;
using Contracts.Users.Requests;
using Contracts.Users.Responses;
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

    public static GetMeResponseDto MapToDto(this DbUser dbUser)
    {
        return new GetMeResponseDto
        {
            Id = dbUser.Id,
            Name = dbUser.Name,
            Surname = dbUser.Surname,
            Email = dbUser.Email,
            ImageId = dbUser.ImageId,
            Patronymic = dbUser.Patronymic,
            Role = dbUser.Role
        };
    }
}