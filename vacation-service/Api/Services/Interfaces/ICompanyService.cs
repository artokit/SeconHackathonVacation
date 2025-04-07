using Api.Dto.Companies.Request;
using Api.Dto.Companies.Responses;
using DataAccess.Models;

namespace Api.Services.Interfaces;

public interface ICompanyService
{
    Task<bool> CreateCompanyAsync(CreateCompanyRequestDto dto);
    Task<GetCompanyResponseDto?> GetCompanyByIdAsync(Guid id);
    Task<GetCompanyResponseDto?> UpdateCompanyAsync(Guid id, UpdateCompanyRequestDto dto);
    Task<bool> DeleteCompanyAsync(Guid id);

}