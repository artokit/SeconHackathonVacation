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
    
    public static DbDepartment MapToDb(this UpdateDepartmentRequestDto requestDto)
    {
        return new DbDepartment
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            SupervisorId = requestDto.SupervisorId,
            ImageId = requestDto.ImageId
        };
    }


    public static List<GetDepartmentResponseDto> MapToDto(this List<DbDepartment> dbDepartments)
    {
        return dbDepartments.Select(x => x.MapToDto()).ToList();
    }
        
    public static GetDepartmentResponseDto MapToDto(this DbDepartment dbDepartment)
    {
        return new GetDepartmentResponseDto
        {
            Id = dbDepartment.Id,
            Name = dbDepartment.Name,
            Description = dbDepartment.Description,
            SupervisorId = dbDepartment.SupervisorId,
            CompanyId = dbDepartment.CompanyId,
            ImageId = dbDepartment.ImageId
        };
    }
}