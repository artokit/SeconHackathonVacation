using Api.Dto.Companies.Request;
using Api.Dto.Companies.Responses;
using DataAccess.Models;

namespace Api.Mappers;

public static class CompanyMappers
{
    public static DbCompany MapToDb(this CreateCompanyRequestDto requestDto)
    {
        return new DbCompany
        {
            Id = Guid.NewGuid(),
            Name = requestDto.Name,
            Description = requestDto.Description,
            Supervisor_Id = requestDto.SupervisorId
        };
    }

    public static DbCompany MapToDb(this UpdateCompanyRequestDto requestDto)
    {
        return new DbCompany
        {
            Name = requestDto.Name,
            Description = requestDto.Description,
            Supervisor_Id = requestDto.SupervisorId
        };
    }

    public static GetCompanyResponseDto MapToDto(this DbCompany dbCompany)
    {
        return new GetCompanyResponseDto
        {
            Id = dbCompany.Id,
            Name = dbCompany.Name,
            Description = dbCompany.Description,
            SupervisorId = dbCompany.Supervisor_Id
        };
    }

    public static List<GetCompanyResponseDto> MapToDto(this List<DbCompany?> dbCompanies)
    {
        return dbCompanies
            .Where(c => c != null)
            .Select(c => c!.MapToDto())
            .ToList();
    }
}
