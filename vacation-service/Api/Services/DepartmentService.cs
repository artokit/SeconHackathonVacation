using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;
using Api.Exceptions.Departments;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using DataAccess.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<GetDepartmentResponseDto> CreateAsync(CreateDepartmentRequestDto registerRequestDto)
    {
        if (await _usersRepository.GetByIdAsync(registerRequestDto.SupervisorId) is null)
        {
            throw new SupervisorNotFound();
        }
        var dbDepartment = registerRequestDto.MapToDb();

        var res = await _departmentsRepository.CreateDepartmentAsync(dbDepartment);
        return res.MapToDto();

    }

    public async Task<GetDepartmentResponseDto> GetByIdAsync(Guid id)
    {
        var res = await _departmentsRepository.GetDepartmentsByIdAsync(id);
        
        if (res is null)
        {
            throw new DepartmentNotFound();
        }
        
        return res.MapToDto();
    }

    public async Task<List<GetDepartmentResponseDto>> GetAllAsync()
    {
        var res = await _departmentsRepository.GetAllDepartments();

        if (res.Any())
        {
            throw new CompanyIsNotExistDepartments();
        }

        return res.MapToDto();
    }

    public async Task<GetDepartmentResponseDto> UpdateAsync(Guid id, UpdateDepartmentRequestDto updateDepartmentRequestDto)
    {
        if (await _departmentsRepository.GetDepartmentsByIdAsync(id) is null)
        {
            throw new DepartmentNotFound();
        }
        
        if (await _usersRepository.GetByIdAsync(updateDepartmentRequestDto.SupervisorId) is null)
        {
            throw new SupervisorNotFound();
        }
        
        var dbDepartment = updateDepartmentRequestDto.MapToDb();

        var res = await _departmentsRepository.UpdateDepartmentAsync(id, dbDepartment);
        return res.MapToDto();
    }

    public async Task DeleteAsync(Guid id)
    {
        if (await _departmentsRepository.GetDepartmentsByIdAsync(id) is null)
        {
            throw new DepartmentNotFound();
        }
        
        await _departmentsRepository.DeleteDepartmentAsync(id);
    }
}