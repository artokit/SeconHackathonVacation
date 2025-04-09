using Api.Dto.Companies.Request;
using Api.Dto.Companies.Responses;
using DataAccess.Models;

namespace Api.Services.Interfaces;

public interface ICompanyService
{
    Task<GetCompanyResponseDto> CreateCompanyAsync(Guid userId, CreateCompanyRequestDto dto);
    Task<GetCompanyResponseDto> GetCompanyByUserIdAsync(Guid userId);
    Task<GetCompanyResponseDto> UpdateCompanyAsync(Guid userId, Guid id, UpdateCompanyRequestDto dto);
}