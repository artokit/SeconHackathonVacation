using DataAccess.Models;

namespace Api.Services.Interfaces;

public interface ICompanyService
{
    public Task<bool> CreateCompanyAsync(DbCompany companyModel);
    public Task<DbCompany> GetCompanyByIdAsync(Guid companyId);
    public Task<DbCompany> UpdateCompanyAsync(Guid companyId, DbCompany updatedCompany);
    public Task<bool> DeleteCompanyAsync(Guid companyId);
}