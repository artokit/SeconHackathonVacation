using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class SchemasRepository : ISchemasRepository
    {
        private readonly IDapperContext _dapperContext;
        
        public SchemasRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<DbSchema> AddAsync(DbSchema dbSchema)
        {
            var queryObject = new QueryObject(
                @"INSERT INTO schemas(user_id, name) 
                VALUES(@UserId, @Name) 
                RETURNING id as ""Id"", user_id as ""UserId"", name as ""Name""",
                new
                {
                    dbSchema.UserId,
                    dbSchema.Name
                });
            
            return await _dapperContext.CommandWithResponse<DbSchema>(queryObject);
        }

        public async Task<DbSchema?> GetByIdAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"SELECT id as ""Id"", user_id as ""UserId"", name as ""Name"" FROM schemas 
                WHERE id = @Id",
                new { Id = id });
            
            return await _dapperContext.FirstOrDefault<DbSchema>(queryObject);
        }

        public async Task DeleteAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"DELETE FROM schemas WHERE id = @Id",
                new { Id = id });
            
            await _dapperContext.Command(queryObject);
        }

        public async Task<DbSchema> UpdateAsync(DbSchema schema)
        {
            var queryObject = new QueryObject(
                @"UPDATE schemas SET name = @Name WHERE id = @Id
                RETURNING id as ""Id"", user_id as ""UserId"", name as ""Name""",
                new
                {
                    schema.Id,
                    schema.Name
                });
        
            return await _dapperContext.CommandWithResponse<DbSchema>(queryObject);
        }
    }