using Contracts.Departments.Responses;

namespace Services.Common.Interfaces;

public interface IDepartmentService
{
    public GetDepartmentResponseDto CreateAsync();  
    public GetDepartmentResponseDto GetByIdAsync();
    public GetDepartmentResponseDto GetAllAsync();
    public GetDepartmentResponseDto UpdateAsync();
    public GetDepartmentResponseDto DeleteAsync();
    
    
}