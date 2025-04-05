using Api.Dto.Employees.Requests;
using Api.Dto.Employees.Responses;

namespace Api.Services.Interfaces;

public interface IEmployeeService
{
    public Task<GetEmployeeResponseDto> CreateAsync(Guid userId, CreateEmployeeRequestDto request);
    public Task DeleteAsync(Guid userId, Guid employeeId);
    public Task<GetEmployeeResponseDto> UpdateAsync(Guid userId, Guid employeeId, UpdateEmployeeRequestDto request);
    public Task<List<GetEmployeeResponseDto>> GetByDepartmentIdAsync(Guid departmentId);
}