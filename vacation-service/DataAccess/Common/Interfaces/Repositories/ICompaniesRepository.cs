using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories
{
    public interface ICompaniesRepository
    {
        public Task<DbCompany?> GetCompanyByIdAsync(Guid id);
        public Task<DbCompany> CreateCompanyAsync(DbCompany company);
        public Task<DbCompany> UpdateCompanyAsync(DbCompany company);
        public Task<DbCompany?> GetCompanyByDirectorId(Guid directorId);
    }
}
