using Contracts.Departments.Requests;
using DataAccess.Models;

namespace Services.Mappers;

public class DepartmentMappers
{
    public static DbDepartment MapToDb(CreateDepartmentRequestdto requestDto)
    {
        return new DbDepartment
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            SupervisorId = requestDto.SupervisorId
        };
    }
}