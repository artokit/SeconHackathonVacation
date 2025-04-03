using DataAccess.Models;

namespace Services.Interfaces
{
    public interface ICompanyService
    {
        public Task<bool> CreateCompanyAsync(CompanyModel companyModel);
        public Task<CompanyModel> GetCompanyByIdAsync(Guid companyId);
        public Task<CompanyModel> UpdateCompanyAsync(Guid companyId, CompanyModel updatedCompany);
        public Task<bool> DeleteCompanyAsync(Guid companyId);
    }
}
