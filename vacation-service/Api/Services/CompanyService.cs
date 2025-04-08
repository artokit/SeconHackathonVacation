using Api.Dto.Companies.Request;
using Api.Dto.Companies.Responses;
using Api.Exceptions.Users;
using Api.Exceptions.Companies;
using Api.Mappers;
using Api.Services.Interfaces;
using DataAccess.Common.Interfaces.Repositories;

namespace Api.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUsersRepository _usersRepository;

    public CompanyService(ICompanyRepository companyRepository, IUsersRepository usersRepository)
    {
        _companyRepository = companyRepository;
        _usersRepository = usersRepository;
    }

    public async Task<bool> CreateCompanyAsync(CreateCompanyRequestDto createDto)
    {
        if (await _usersRepository.GetByIdAsync(createDto.SupervisorId) is null)
        {
            throw new SupervisorNotFound();
        }

        var dbCompany = createDto.MapToDb();
        await _companyRepository.CreateCompanyAsync(dbCompany);
        return true;
    }

    public async Task<GetCompanyResponseDto?> GetCompanyByIdAsync(Guid companyId)
    {
        var company = await _companyRepository.GetCompaniesByIdAsync(companyId);
        return company?.MapToDto();
    }

    public async Task<GetCompanyResponseDto?> UpdateCompanyAsync(Guid companyId, UpdateCompanyRequestDto updateDto)
    {
        if (await _companyRepository.GetCompaniesByIdAsync(companyId) is null)
        {
            return null;
        }

        if (await _usersRepository.GetByIdAsync(updateDto.SupervisorId) is null)
        {
            throw new SupervisorNotFound();
        }

        var dbCompany = updateDto.MapToDb();
        var updated = await _companyRepository.UpdateCompanyAsync(companyId, dbCompany);
        return updated.MapToDto();
    }

    public async Task<bool> DeleteCompanyAsync(Guid companyId)
    {
        var company = await _companyRepository.GetCompaniesByIdAsync(companyId);

        if (company is null)
            return false;

        await _companyRepository.DeleteCompanyAsync(companyId);
        return true;
    }
}
