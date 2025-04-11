using Api.Dto.Users.Requests;
using Api.Dto.Users.Responses;
using Api.Exceptions.Companies;
using Api.Exceptions.Departments;
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
    private readonly IDepartmentsRepository _departmentsRepository;
    private readonly IFileServiceClient _fileServiceClient;
    private readonly ICompaniesRepository _companiesRepository;
    
    public UserService(IUsersRepository usersRepository, INotificationServiceClient notificationServiceClient, IDepartmentsRepository departmentsRepository, IFileServiceClient fileServiceClient, ICompaniesRepository companiesRepository)
    {
        _usersRepository = usersRepository;
        _notificationServiceClient = notificationServiceClient;
        _departmentsRepository = departmentsRepository;
        _fileServiceClient = fileServiceClient;
        _companiesRepository = companiesRepository;
    }

    public async Task<GetMeResponseDto> GetMe(Guid userId)
    {
        var res = await _usersRepository.GetByIdAsync(userId);
        
        if (res is null)
        {
            throw new UserNotFoundRequest();
        }

        var userDto = res.MapToGetMeDto();
        
        if (res.DepartmentId is not null)
        {
            var department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)res.DepartmentId);

            if (department is not null)
            {
                var company = await _companiesRepository.GetCompanyByIdAsync(department.CompanyId);
                
                if (company is null)
                {
                    throw new CompanyNotFoundException();
                }
                
                userDto.Company = company.MapToDto();
            }
        }

        return userDto;
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
        
        // Проверка на сущесвование пользователей.
        if (user is null || userToUpdate is null)
        {
            throw new UserNotFoundRequest();
        }

        if (user.Role is not Roles.Director && user.Role is not Roles.Hr && user.Id != request.UserId)
        {
            throw new CantEditUserException();
        }
        
        var currentDepartmentTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)userToUpdate.DepartmentId!);
        var userDepartmentTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId!);
        
        await Task.WhenAll(currentDepartmentTask, userDepartmentTask);

        var currentDepartment = currentDepartmentTask.Result;
        var userDepartment = userDepartmentTask.Result;

        if (currentDepartment is null || userDepartment is null)
        {
            throw new DepartmentNotFoundRequest();
        }
        
        // Проверка что пользователи в одной компании.
        if (user.Id != userToUpdate.Id && userDepartment.CompanyId != currentDepartment.CompanyId)
        {
            throw new UserNotFoundRequest();
        }
        
        // Проверка на то, что пользователю хотят поменять отдел.
        if (request.DepartmentId is not null)
        {
            // Проверка, что у человека есть права на то, чтобы поменять отдел.
            if (user.Role is not Roles.Director && user.Role is not Roles.Hr)
            {
                throw new CantEditDepartmentException();
            }
            
            var updatedDepartment = await _departmentsRepository.GetDepartmentByIdAsync((Guid)request.DepartmentId!);
            
            if (updatedDepartment is null)
            {
                throw new DepartmentNotFoundRequest();
            }
            
            // Проверка, что меняют на отдел, который находится в той же компании.
            if (currentDepartment.CompanyId != updatedDepartment.CompanyId)
            {
                throw new UserNotFoundRequest();
            }
        }

        
        Image? image = null;
        
        if (request.ImageId is not null)
        {
            image = await _fileServiceClient.GetImageByIdAsync((Guid)request.ImageId);
            
            if (image is null)
            {
                throw new ImageNotFoundException();
            }
        }
        
        var dbUserToUpdate = request.MapToDb(userToUpdate, image?.Name);
        var updatedUser = await _usersRepository.UpdateAsync(dbUserToUpdate);
        
        return updatedUser.MapToDto();
    }
}