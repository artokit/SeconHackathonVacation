using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;
using Api.Exceptions.Departments;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services;

public class DepartmentService : IDepartmentService
{
    private IDepartmentsRepository _departmentsRepository;
    private IUsersRepository _usersRepository;

    public DepartmentService(IDepartmentsRepository departmentsRepository, IUsersRepository usersRepository)
    {
        _departmentsRepository = departmentsRepository;
        _usersRepository = usersRepository;
    }

    public async Task<GetDepartmentResponseDto> CreateAsync(Guid userId, CreateDepartmentRequestDto registerRequestDto)
    {
        var dbDepartment = registerRequestDto.MapToDb();

        var res = await _departmentsRepository.CreateDepartmentAsync(dbDepartment);
        return res.MapToDto();

    }

    public async Task<GetDepartmentResponseDto> GetByIdAsync(Guid userId, Guid id)
    {
        var res = await _departmentsRepository.GetDepartmentByIdAsync(id);
        
        if (res is null)
        {
            throw new DepartmentNotFoundRequest();
        }
        
        return res.MapToDto();
    }

    public async Task<List<GetDepartmentResponseDto>> GetAllAsync(Guid userId)
    {
        var res = await _departmentsRepository.GetAllDepartments();

        if (res.Any())
        {
            throw new CompanyIsNotExistDepartments();
        }

        return res.MapToDto();
    }

    public async Task<GetDepartmentResponseDto> UpdateAsync(Guid userId, Guid id, UpdateDepartmentRequestDto updateDepartmentRequestDto)
    {
        if (await _departmentsRepository.GetDepartmentByIdAsync(id) is null)
        {
            throw new DepartmentNotFoundRequest();
        }
        
        if (await _usersRepository.GetByIdAsync(updateDepartmentRequestDto.SupervisorId) is null)
        {
            throw new SupervisorNotFoundRequest();
        }
        
        var dbDepartment = updateDepartmentRequestDto.MapToDb();

        var res = await _departmentsRepository.UpdateDepartmentAsync(id, dbDepartment);
        return res.MapToDto();
    }

    public async Task DeleteAsync(Guid userId, Guid id)
    {
        if (await _departmentsRepository.GetDepartmentByIdAsync(id) is null)
        {
            throw new DepartmentNotFoundRequest();
        }
        
        await _departmentsRepository.DeleteDepartmentAsync(id);
    }
}