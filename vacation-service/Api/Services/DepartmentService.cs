using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;
using Api.Exceptions.Departments;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using Application.Common.Interfaces;
using Application.FileService.Models;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services;

public class DepartmentService : IDepartmentService
{
    private IDepartmentsRepository _departmentsRepository;
    private IUsersRepository _usersRepository;
    private IFileServiceClient _fileServiceClient;

    public DepartmentService(IDepartmentsRepository departmentsRepository, IUsersRepository usersRepository, IFileServiceClient fileServiceClient)
    {
        _departmentsRepository = departmentsRepository;
        _usersRepository = usersRepository;
        _fileServiceClient = fileServiceClient;
    }

    public async Task<GetDepartmentResponseDto> CreateAsync(Guid userId, CreateDepartmentRequestDto registerRequestDto)
    {
        var dbDepartment = registerRequestDto.MapToDb();
        
        if (await _usersRepository.GetByIdAsync(registerRequestDto.SupervisorId) is null)
        {
            throw new SupervisorNotFoundRequest();
        }
        
        var supervisorTask = _usersRepository.GetByIdAsync(registerRequestDto.SupervisorId);
        var userTask = _usersRepository.GetByIdAsync(userId);

        await Task.WhenAll(supervisorTask, userTask);

        var user = userTask.Result;
        var supervisor = supervisorTask.Result;

        var userDepartmentTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId);
        var supervisorDepartmentTask = _departmentsRepository.GetDepartmentByIdAsync((Guid)supervisor.DepartmentId);

        await Task.WhenAll(userDepartmentTask, supervisorDepartmentTask);
        
        var userDepartment = userDepartmentTask.Result;
        var supervisorDepartment = supervisorDepartmentTask.Result;

        if (userDepartment is null || supervisorDepartment is null)
        {
            throw new DepartmentNotFoundRequest();
        }
        
        if (userDepartment.CompanyId != supervisorDepartment.CompanyId)
        {
            throw new UserNotFoundRequest();
        }

        dbDepartment.CompanyId = userDepartment.CompanyId;
        
        var res = await _departmentsRepository.CreateDepartmentAsync(dbDepartment);
        
        supervisor.DepartmentId = res.Id;
        await _usersRepository.UpdateAsync(supervisor);
        
        return res.MapToDto();

    }

    public async Task<GetDepartmentFullInfoResponseDto> GetByIdAsync(Guid userId, Guid id)
    {
        var res = await _departmentsRepository.GetDepartmentByIdAsync(id);

        if (res is null)
        {
            throw new DepartmentNotFoundRequest();
        }

        var departmentUsers = await _usersRepository.GetUsersByDepartmentIdAsync(res.Id);
        
        return res.MapToFullInfoDto(departmentUsers);
    }

    public async Task<List<GetDepartmentResponseDto>> GetAllAsync(Guid userId)
    {
        var user = await _usersRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new UserNotFoundRequest();
        }

        if (user.DepartmentId is null)
        {
            throw new UserNotFoundRequest();
        }
        
        var department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId);

        if (department is null)
        {
            throw new DepartmentNotFoundRequest();
        }
        
        var res = await _departmentsRepository.GetAllDepartmentsByCompanyIdAsync(department.CompanyId);


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
        
        Image? image = null;
        if (updateDepartmentRequestDto.ImageId is not null)
        {
            image = await _fileServiceClient.GetImageByIdAsync((Guid)updateDepartmentRequestDto.ImageId);
            
            if (image is null)
            {
                throw new FileNotFoundException();
            }
        }
        
        var dbDepartment = updateDepartmentRequestDto.MapToDb(image?.Name);

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