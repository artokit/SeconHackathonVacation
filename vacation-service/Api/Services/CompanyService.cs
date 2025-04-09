using Api.Dto.Companies.Request;
using Api.Dto.Companies.Responses;
using Api.Exceptions.Users;
using Api.Exceptions.Companies;
using Api.Exceptions.Departments;
using Api.Mappers;
using Api.Services.Interfaces;
using Common;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;

namespace Api.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IDepartmentsRepository _departmentsRepository;
    
    public CompanyService(ICompaniesRepository companiesRepository, IUsersRepository usersRepository, IDepartmentsRepository departmentsRepository)
    {
        _companiesRepository = companiesRepository;
        _usersRepository = usersRepository;
        _departmentsRepository = departmentsRepository;
    }

    public async Task<GetCompanyResponseDto> CreateCompanyAsync(Guid userId, CreateCompanyRequestDto createDto)
    {
        var user = await _usersRepository.GetByIdAsync(userId);
        
        if (user is null)
        {
            throw new UserNotFoundRequest();
        }
        
        if (user.Role != Roles.Director)
        {
            throw new CantCreateCompanyException();
        }
        
        var company = await _companiesRepository.GetCompanyByDirectorId(userId);
        
        if (company is not null)
        {
            throw new CompanyWasCreatedException();
        }
        
        var dbCompany = createDto.MapToDb();
        dbCompany.DirectorId = userId;
        var createdCompany = await _companiesRepository.CreateCompanyAsync(dbCompany);
        
        var dbDepartment = await _departmentsRepository.CreateDepartmentAsync(new DbDepartment
        {
            Name = "Главный отдел",
            Description = "Главный отдел, который создаётся автоматически",
            SupervisorId = userId,
            CompanyId = createdCompany.Id
        });

        user.DepartmentId = dbDepartment.Id;
        await _usersRepository.UpdateAsync(user);
        
        return createdCompany.MapToDto();
    }

    public async Task<GetCompanyResponseDto> GetCompanyByUserIdAsync(Guid userId)
    {
        var user = await _usersRepository.GetByIdAsync(userId);
        
        if (user is null)
        {
            throw new UserNotFoundRequest();
        }

        if (user.DepartmentId == Guid.Empty || user.DepartmentId is null)
        {
            throw new UserNotFoundRequest();
        }
        
        var department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId);

        if (department is null)
        {
            throw new DepartmentNotFoundRequest();
        }
        
        var company = await _companiesRepository.GetCompanyByIdAsync(department.CompanyId);
        
        if (company is null)
        {
            throw new CompanyNotFoundException();
        }
        
        return company.MapToDto();
    }

    public async Task<GetCompanyResponseDto> UpdateCompanyAsync(Guid userId, Guid companyId, UpdateCompanyRequestDto updateDto)
    {
        var dbCompany = await _companiesRepository.GetCompanyByIdAsync(companyId);
        
        if (dbCompany is null)
        {
            throw new CompanyNotFoundException();
        }

        if (dbCompany.DirectorId != userId)
        {
            throw new CantUpdateCompanyException();
        }

        var dbCompanyToUpdate = updateDto.MapToDb(dbCompany);
        var updated = await _companiesRepository.UpdateCompanyAsync(dbCompanyToUpdate);
        return updated.MapToDto();
    }
}
