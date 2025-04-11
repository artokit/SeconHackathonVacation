using Api.Dto.Users.Requests;
using Api.Dto.Users.Responses;
using Api.Exceptions.Images;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using Application.Common.Interfaces;
using Application.FileService.Models;
using Application.NotificationService.Models;
using Common;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services;


public class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly INotificationServiceClient _notificationServiceClient;
    private readonly IFileServiceClient _fileServiceClient;
    private readonly IRightService _rightService;
        
    public UserService(
        IUsersRepository usersRepository,
        INotificationServiceClient notificationServiceClient,
        IFileServiceClient fileServiceClient,
        IRightService rightService)
    {
        _usersRepository = usersRepository;
        _notificationServiceClient = notificationServiceClient;
        _fileServiceClient = fileServiceClient;
        _rightService = rightService;
    }

    public async Task<GetUserResponseDto> GetMe(Guid userId)
    {
        var res = await _usersRepository.GetByIdAsync(userId) ?? throw new UserNotFoundRequest();
        return res.MapToGetMeDto();
    }
        
    public async Task<GetUserResponseDto> CreateAsync(Guid userId, CreateUserRequestDto request)
    {
        if (await _usersRepository.GetByEmailAsync(request.Email) is not null)
        {
            throw new EmailIsExistException();
        }

        await _rightService.ValidateUserCreationRightsAsync(userId, request.DepartmentId, request.Role);

        var generatedPassword = PasswordService.GeneratePassword();
        Console.WriteLine(generatedPassword);
            
        var res = await _usersRepository.AddAsync(request.MapToDb(generatedPassword));
            
        await _notificationServiceClient.SendGeneratedPasswordAsync(new GeneratedPasswordRequest
        {
            ToEmail = request.Email,
            Password = generatedPassword
        });
            
        return res.MapToDto();
    }

    public async Task DeleteAsync(Guid userId, Guid employeeId)
    {
        await _rightService.ValidateUserDeletionRightsAsync(userId, employeeId);
        await _usersRepository.DeleteAsync(employeeId);
    }

    public async Task<GetUserResponseDto> UpdateAsync(Guid userId, UpdateUserRequestDto request)
    {
        await _rightService.ValidateUserUpdateRightsAsync(userId, request.UserId, request.DepartmentId);

        var userToUpdate = await _usersRepository.GetByIdAsync(request.UserId) 
                           ?? throw new UserNotFoundRequest();

        Image? image = null;
        if (request.ImageId is not null)
        {
            image = await _fileServiceClient.GetImageByIdAsync((Guid)request.ImageId);
            if (image is null) throw new ImageNotFoundException();
        }
            
        var dbUserToUpdate = request.MapToDb(userToUpdate, image?.Name);
        var updatedUser = await _usersRepository.UpdateAsync(dbUserToUpdate);
            
        return updatedUser.MapToDto();
    }
}