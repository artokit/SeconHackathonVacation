using Common;
using Contracts.Employees.Requests;
using Contracts.Employees.Responses;
using DataAccess.Common.Interfaces.Repositories;
using Services.Common.Interfaces;
using Services.Exceptions.Users;
using Services.Mappers;

namespace Services.Employees;

public class EmployeesService : IEmployeesService
{
    private IUsersRepository _usersRepository;
    
    public EmployeesService(IUsersRepository usersRepository)
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