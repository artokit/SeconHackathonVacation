using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;
using Api.Dto.Users.Responses;
using DataAccess.Models;

namespace Api.Mappers;

public static class DepartmentMappers
{
    public static DbDepartment MapToDb(this CreateDepartmentRequestDto requestDto)
    {
        return new DbDepartment
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            SupervisorId = requestDto.SupervisorId
        };
    }
    
    public static GetDepartmentResponseDto MapToDto(this DbDepartment requestDto)
    {
        return new GetDepartmentResponseDto
        {
            Id = requestDto.Id,
            Name = requestDto.Name,
            Description = requestDto.Description,
            SupervisorId = requestDto.SupervisorId
        };
    }
}