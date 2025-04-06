using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;

namespace Api.Services.Interfaces;

public interface IDepartmentService
{
    public Task<GetDepartmentResponseDto> CreateAsync(CreateDepartmentRequestDto createDepartmentRequestDto);  
    public Task<GetDepartmentResponseDto> GetByIdAsync(Guid id);
    public Task<List<GetDepartmentResponseDto>> GetAllAsync();
    public Task<GetDepartmentResponseDto> UpdateAsync(Guid id, UpdateDepartmentRequestDto updateDepartmentRequestDto);
    public Task DeleteAsync(Guid id);
    
    
}