using Api.Services.Interfaces;
using DataAccess.Models;

namespace Api.Services;


public class CompanyService : ICompanyService
{
    public Task<bool> CreateCompanyAsync(DbCompany companyModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCompanyAsync(Guid companyId)
    {
        throw new NotImplementedException();
    }

    public Task<DbCompany> GetCompanyByIdAsync(Guid companyId)
    {
        throw new NotImplementedException();
    }

    public Task<DbCompany> UpdateCompanyAsync(Guid companyId, DbCompany updatedCompany)
    {
        throw new NotImplementedException();
    }
}