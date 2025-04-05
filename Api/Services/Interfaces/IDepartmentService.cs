using Api.Dto.Departments.Responses;

namespace Api.Services.Interfaces;

public interface IDepartmentService
{
    public GetDepartmentResponseDto CreateAsync();  
    public GetDepartmentResponseDto GetByIdAsync();
    public GetDepartmentResponseDto GetAllAsync();
    public GetDepartmentResponseDto UpdateAsync();
    public GetDepartmentResponseDto DeleteAsync();
    
    
}