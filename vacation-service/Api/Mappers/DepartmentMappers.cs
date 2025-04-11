using Api.Dto.Departments.Requests;
using Api.Dto.Departments.Responses;
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
    
    public static DbDepartment MapToDb(this UpdateDepartmentRequestDto requestDto, string? imageName)
    {
        return new DbDepartment
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            SupervisorId = requestDto.SupervisorId,
            // ImageName = requestDto.ImageName
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
            ImageName = dbDepartment.ImageName
        };
    }

    public static GetDepartmentFullInfoResponseDto MapToFullInfoDto(this DbDepartment dbDepartment,
        List<DbUser> departmentUsers)
    {
        return new GetDepartmentFullInfoResponseDto
        {
            Id = dbDepartment.Id,
            Name = dbDepartment.Name,
            Description = dbDepartment.Description,
            SupervisorId = dbDepartment.SupervisorId,
            CompanyId = dbDepartment.CompanyId,
            ImageName = dbDepartment.ImageName,
            Employees = departmentUsers.Select(u => u.MapToDto()).ToList()
        };
    }
}