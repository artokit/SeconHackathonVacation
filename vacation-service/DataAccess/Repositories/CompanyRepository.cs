using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;
using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Dapper;

namespace DataAccess.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDapperContext _dapperContext;

        public CompanyRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<DbCompany?> GetCompaniesByIdAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"SELECT id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""Supervisor_Id""
                  FROM companies
                  WHERE id = @id",
                new { id });
            return await _dapperContext.FirstOrDefault<DbCompany>(queryObject);
        }

        public async Task<DbCompany> CreateCompanyAsync(DbCompany company)
        {
            var queryObject = new QueryObject(
                @"INSERT INTO companies (name, description, supervisor_id)
                  VALUES (@Name, @Description, @Supervisor_Id)
                  RETURNING id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""Supervisor_Id""",
                new
                {
                    company.Name,
                    company.Description,
                    company.Supervisor_Id
                });
            return await _dapperContext.CommandWithResponse<DbCompany>(queryObject);
        }

        public async Task<DbCompany> UpdateCompanyAsync(Guid id, DbCompany company)
        {
            var queryObject = new QueryObject(
                @"UPDATE companies
                  SET name = @Name, description = @Description, supervisor_id = @Supervisor_Id
                  WHERE id = @Id
                  RETURNING id as ""Id"", name as ""Name"", description as ""Description"", supervisor_id as ""Supervisor_Id""",
                new
                {
                    Id = id,
                    company.Name,
                    company.Description,
                    company.Supervisor_Id
                });
            return await _dapperContext.CommandWithResponse<DbCompany>(queryObject);
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"DELETE FROM companies WHERE id = @id",
                new { id });
            await _dapperContext.Command(queryObject);
        }
    }
}