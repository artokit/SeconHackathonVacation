using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        public Task<DbCompany?> GetCompaniesByIdAsync(Guid id);
        public Task<List<DbCompany?>> GetAllCompanies();
        public Task<DbCompany> CreateCompanyAsync(DbCompany company);
        public Task<DbCompany> UpdateCompanyAsync(Guid id, DbCompany company);
        public Task DeleteCompanyAsync(Guid id);
    }
}
