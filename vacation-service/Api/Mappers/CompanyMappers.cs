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
        };
    }

    public static DbCompany MapToDb(this UpdateCompanyRequestDto requestDto, DbCompany dbCompany)
    {
        return new DbCompany
        {
            Id = dbCompany.Id,
            Name = requestDto.Name ?? dbCompany.Name,
            Description = requestDto.Description ?? dbCompany.Description,
            DirectorId = dbCompany.DirectorId 
        };
    }

    public static GetCompanyResponseDto MapToDto(this DbCompany dbCompany)
    {
        return new GetCompanyResponseDto
        {
            Id = dbCompany.Id,
            Name = dbCompany.Name,
            Description = dbCompany.Description,
            DirectorId = dbCompany.DirectorId
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
