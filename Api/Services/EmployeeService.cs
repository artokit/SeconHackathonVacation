using Api.Dto.Employees.Requests;
using Api.Dto.Employees.Responses;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using Common;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services;

public class EmployeeService : IEmployeeService
{
    private IUsersRepository _usersRepository;
    
    public EmployeeService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<GetEmployeeResponseDto> CreateAsync(Guid userId, CreateEmployeeRequestDto request)
    {
        if (await _usersRepository.GetByEmailAsync(request.Email) is not null)
        {
            throw new EmailIsExistException();
        }

        var generatedPassword = PasswordService.GeneratePassword();

        var res = await _usersRepository.AddAsync(request.MapToDb(generatedPassword));
        await NotifyUserByEmail(request.Email);
        
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

    public Task<GetEmployeeResponseDto> UpdateAsync(Guid userId, Guid employeeId, UpdateEmployeeRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetEmployeeResponseDto>> GetByDepartmentIdAsync(Guid departmentId)
    {
        throw new NotImplementedException();
    }
    
    private async Task NotifyUserByEmail(string email)
    {
        Console.WriteLine($"{email} оповещено");
    }
}