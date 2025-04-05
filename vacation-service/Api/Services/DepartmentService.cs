using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;
using Api.Mappers;
using Api.Services.Interfaces;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services;

public class DepartmentService : IDepartmentService
{
    private IDepartmentsRepository _departmentsRepository;

    public DepartmentService(IDepartmentsRepository departmentsRepository)
    {
        _departmentsRepository = departmentsRepository;
    }

    public async Task<GetDepartmentResponseDto> CreateAsync(CreateDepartmentRequestDto registerRequestDto)
    {

        var dbDepartment = registerRequestDto.MapToDb();

        var res = await _departmentsRepository.CreateDepartmentAsync(dbDepartment);
        return res.MapToDto();

    }

    public Task<GetDepartmentResponseDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<GetDepartmentResponseDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetDepartmentResponseDto> UpdateAsync(UpdateDepartmentRequestDto updateDepartmentRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}