using Common;
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
            HashedPassoword = requestDto.Password.Hash(),
            Email = requestDto.Email
        };
    }
}