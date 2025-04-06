using Api.Dto.Users.Requests;
using Api.Dto.Users.Responses;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using Application.Common.Interfaces;
using Application.NotificationService.Models;
using Common;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services;


public class UserService : IUserService
{
    private IUsersRepository _usersRepository;
    private INotificationServiceClient _notificationServiceClient;
    
    public UserService(IUsersRepository usersRepository, INotificationServiceClient notificationServiceClient)
    {
        _usersRepository = usersRepository;
        _notificationServiceClient = notificationServiceClient;
    }

    public async Task<GetUserResponseDto> GetMe(Guid userId)
    {
        var res = await _usersRepository.GetByIdAsync(userId);
        
        if (res is null)
        {
            throw new UserNotFound();
        }

        return res.MapToDto();
    }
    
    public async Task<GetUserResponseDto> CreateAsync(Guid userId, CreateUserRequestDto request)
    {
        if (await _usersRepository.GetByEmailAsync(request.Email) is not null)
        {
            throw new EmailIsExistException();
        }

        var generatedPassword = PasswordService.GeneratePassword();

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
        // ToDo: добавить проверку, что челик, который отправил запрос, находится в этом же отделе.
        
        if (await _usersRepository.GetByIdAsync(employeeId) is null)
        {
            throw new UserNotFound();
        }
        
        await _usersRepository.DeleteAsync(employeeId);
    }

    public Task<GetUserResponseDto> UpdateAsync(Guid userId, Guid employeeId, UpdateUserRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetUserResponseDto>> GetByDepartmentIdAsync(Guid departmentId)
    {
        throw new NotImplementedException();
    }
}