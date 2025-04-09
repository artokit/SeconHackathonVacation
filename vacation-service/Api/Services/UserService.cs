using Api.Dto.Users.Requests;
using Api.Dto.Users.Responses;
using Api.Exceptions.Companies;
using Api.Exceptions.Departments;
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
    private readonly IUsersRepository _usersRepository;
    private readonly INotificationServiceClient _notificationServiceClient;
    private readonly IDepartmentsRepository _departmentsRepository;
    private readonly IFileServiceClient _fileServiceClient;
    
    public UserService(IUsersRepository usersRepository, INotificationServiceClient notificationServiceClient, IDepartmentsRepository departmentsRepository, IFileServiceClient fileServiceClient)
    {
        _usersRepository = usersRepository;
        _notificationServiceClient = notificationServiceClient;
        _departmentsRepository = departmentsRepository;
        _fileServiceClient = fileServiceClient;
    }

    public async Task<GetUserResponseDto> GetMe(Guid userId)
    {
        var res = await _usersRepository.GetByIdAsync(userId);
        
        if (res is null)
        {
            throw new UserNotFoundRequest();
        }

        return res.MapToDto();
    }
    
    public async Task<GetUserResponseDto> CreateAsync(Guid userId, CreateUserRequestDto request)
    {
        // Валидация данных
        if (request.Role == Roles.Director)
        {
            throw new CantCreateDirectorException();
        }
        
        if (await _usersRepository.GetByEmailAsync(request.Email) is not null)
        {
            throw new EmailIsExistException();
        }
        

        var departmentTask = _departmentsRepository.GetDepartmentByIdAsync(request.DepartmentId);
        var userTask = _usersRepository.GetByIdAsync(userId);
        
        await Task.WhenAll(departmentTask, userTask);

        var user = userTask.Result;
        var department = departmentTask.Result;

        if (user is null)
        {
            throw new UserNotFoundRequest();
        }
        
        if (department is null)
        {
            throw new DepartmentNotFoundRequest();
        }

        if (department.SupervisorId != userId && user.Role != Roles.Director && user.Role != Roles.Hr)
        {
            throw new CantAddUserToAnotherDepartment();
        }

        
        var inviterUserDepartment = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId!);
        
        if (inviterUserDepartment is null)
        {
            throw new CantCreateCompanyException();
        }

        // Проверка, что пользователя добавляют в отдел, который существует в рамках компании.
        if (inviterUserDepartment.CompanyId != department.CompanyId)
        {
            throw new DepartmentNotFoundRequest();
        }
        
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
        var user = await _usersRepository.GetByIdAsync(userId);

        if (user.Role != Roles.Director && user.Role != Roles.Hr)
        {
            throw new CantDeleteUserException();
        }

        var userToDelete = await _usersRepository.GetByIdAsync(employeeId);
        
        if (userToDelete is null)
        {
            throw new UserNotFoundRequest();
        }

        var departmentUserToDeleteTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)userToDelete.DepartmentId!);
        var departmentUserTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId!);

        await Task.WhenAll(departmentUserToDeleteTask, departmentUserTask);

        var departmentUser = departmentUserTask.Result;
        var departmentUserToDelete = departmentUserToDeleteTask.Result;
        
        if (departmentUser.CompanyId != departmentUserToDelete.CompanyId)
        {
            throw new UserNotFoundRequest();
        }
        
        await _usersRepository.DeleteAsync(employeeId);
    }

    public async Task<GetUserResponseDto> UpdateAsync(Guid userId, UpdateUserRequestDto request)
    {
        var user = await _usersRepository.GetByIdAsync(userId);
        var userToUpdate = await _usersRepository.GetByIdAsync(request.UserId);
        
        if (user is null || userToUpdate is null)
        {
            throw new UserNotFoundRequest();
        }
        
        if (request.DepartmentId is not null)
        {
            var currentDepartmentTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId!);
            var updatedDepartmentTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)request.DepartmentId);

            await Task.WhenAll(currentDepartmentTask, updatedDepartmentTask);

            var currentDepartment = currentDepartmentTask.Result;
            var updatedDepartment = updatedDepartmentTask.Result;

            if (currentDepartment.CompanyId != updatedDepartment.CompanyId)
            {
                throw new DepartmentNotFoundRequest();
            }
        }

        if (request.ImageId is not null)
        {
            var image = await _fileServiceClient.GetImageById((Guid)request.ImageId);
            
            if (image is null)
            {
                throw new FileNotFoundException();
            }
        }
        
        var dbUserToUpdate = request.MapToDb(userToUpdate);
        var updatedUser = await _usersRepository.UpdateAsync(dbUserToUpdate);
        
        return updatedUser.MapToDto();
    }

    public Task<List<GetUserResponseDto>> GetByDepartmentIdAsync(Guid departmentId)
    {
        throw new NotImplementedException();
    }
}