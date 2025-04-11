using Api.Dto.Authorization.Requests;
using Api.Dto.Users.Requests;
using Api.Dto.Users.Responses;
using Application.Settings;
using Common;
using DataAccess.Models;

namespace Api.Mappers;

public static class UserMappers
{
    public static ApplicationSettings ApplicationSettings { get; private set; }
 
    public static void Initilize(ApplicationSettings applicationSettings)
    {
        ApplicationSettings = applicationSettings;
    }
    
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
            ImageUrl = dbUser.ImageName is not null ? $"{ApplicationSettings.FileS3CloudServiceUrl}/{dbUser.ImageName}" : null,
            Role = dbUser.Role
        };
    }
    
    public static GetUserResponseDto MapToGetMeDto(this DbUser dbUser)
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
            ImageUrl =  dbUser.ImageName is not null ? $"{ApplicationSettings.FileS3CloudServiceUrl}/{dbUser.ImageName}" : null,
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
            Role = request.Role,
            DepartmentId = request.DepartmentId
        };
    }

    public static DbUser MapToDb(this UpdateUserRequestDto user, DbUser dbUser, string? imageName)
    {
        return new DbUser
        {
            Id = dbUser.Id,
            Name = user.Name ?? dbUser.Name,
            Surname = user.Surname ?? dbUser.Surname,
            Patronymic = user.Patronymic ?? dbUser.Patronymic,
            ImageName = imageName ?? dbUser.ImageName,
            HashedPassword = dbUser.HashedPassword,
            Phone = user.Phone ?? dbUser.Phone,
            TelegramUsername = user.TelegramUsername ?? dbUser.TelegramUsername,
            DepartmentId = user.DepartmentId ?? dbUser.DepartmentId,
            Email = dbUser.Email,
            Role = dbUser.Role
        };
    }
}