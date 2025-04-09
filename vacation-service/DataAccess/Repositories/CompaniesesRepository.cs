using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;
using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Dapper;

namespace DataAccess.Repositories
{
    public class CompaniesesRepository : ICompaniesRepository
    {
        private readonly IDapperContext _dapperContext;

        public CompaniesesRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<DbCompany?> GetCompanyByIdAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"SELECT id as ""Id"", name as ""Name"", description as ""Description"", director_id as ""DirectorId""
                  FROM companies
                  WHERE id = @id",
                new { id });
            return await _dapperContext.FirstOrDefault<DbCompany>(queryObject);
        }

        public async Task<DbCompany> CreateCompanyAsync(DbCompany company)
        {
            var queryObject = new QueryObject(
                @"INSERT INTO companies (name, description, director_id)
                  VALUES (@Name, @Description, @DirectorId)
                  RETURNING id as ""Id"", name as ""Name"", description as ""Description"", director_id as ""DirectorId""",
                new
                {
                    company.Name,
                    company.Description,
                    company.DirectorId
                });
            return await _dapperContext.CommandWithResponse<DbCompany>(queryObject);
        }

        public async Task<DbCompany> UpdateCompanyAsync(DbCompany company)
        {
            var queryObject = new QueryObject(
                @"UPDATE companies
                  SET name = @Name, description = @Description, director_id = @DirectorId
                  WHERE id = @Id
                  RETURNING id as ""Id"", name as ""Name"", description as ""Description"", director_id as ""DirectorId""",
                new
                {
                    Id = company.Id,
                    company.Name,
                    company.Description,
                    company.DirectorId
                });
            return await _dapperContext.CommandWithResponse<DbCompany>(queryObject);
        }

        public async Task<DbCompany?> GetCompanyByDirectorId(Guid directorId)
        {
            var queryObject =
                new QueryObject(
                    @"SELECT id as ""Id"", name as ""Name"", description as ""Name"", director_id as ""DirectorId"" FROM companies WHERE director_id = @directorId",
                    new
                    {
                        directorId = directorId
                    });
            return await _dapperContext.FirstOrDefault<DbCompany>(queryObject);
        }
    }
}