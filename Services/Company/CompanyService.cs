using DataAccess.Models;
using Services.Interfaces;

namespace Services.Company
{
    public class CompanyService : ICompanyService
    {
        public Task<bool> CreateCompanyAsync(CompanyModel companyModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCompanyAsync(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyModel> GetCompanyByIdAsync(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyModel> UpdateCompanyAsync(Guid companyId, CompanyModel updatedCompany)
        {
            throw new NotImplementedException();
        }
    }
}
