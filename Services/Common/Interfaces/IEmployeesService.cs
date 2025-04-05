using Contracts.Employees.Requests;
using Contracts.Employees.Responses;

namespace Services.Common.Interfaces;

public interface IEmployeesService
{
    public Task<GetEmployeeResponseDto> CreateAsync(Guid userId, CreateEmployeeRequestDto request);
    public Task DeleteAsync(Guid userId, Guid employeeId);
    public Task<GetEmployeeResponseDto> UpdateAsync(Guid userId, Guid employeeId, UpdateEmployeeRequestDto request);
    public Task<List<GetEmployeeResponseDto>> GetByDepartmentIdAsync(Guid departmentId);
}