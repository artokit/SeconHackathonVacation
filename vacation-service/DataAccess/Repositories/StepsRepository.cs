using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class StepsRepository : IStepsRepository
    {
        private readonly IDapperContext _dapperContext;
        
        public StepsRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<DbStep> AddAsync(DbStep dbStep)
        {
            var queryObject = new QueryObject(
                @"INSERT INTO steps(schema_id, approver_id) 
                VALUES(@SchemaId, @ApproverId) 
                RETURNING id as ""Id"", schema_id as ""SchemaId"", approver_id as ""ApproverId""",
                new
                {
                    dbStep.SchemaId,
                    dbStep.ApproverId
                });
            
            return await _dapperContext.CommandWithResponse<DbStep>(queryObject);
        }

        public async Task<DbStep?> GetByIdAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"SELECT id as ""Id"", schema_id as ""SchemaId"", approver_id as ""ApproverId"" FROM steps 
                WHERE id = @Id",
                new { Id = id });
            
            return await _dapperContext.FirstOrDefault<DbStep>(queryObject);
        }

        public async Task<List<DbStep>> GetBySchemaIdAsync(Guid schemaId)
        {
            var queryObject = new QueryObject(
                @"SELECT id as ""Id"", schema_id as ""SchemaId"", approver_id as ""ApproverId"" FROM steps 
                WHERE schema_id = @SchemaId",
                new { SchemaId = schemaId });
            
            return await _dapperContext.ListOrEmpty<DbStep>(queryObject);
        }

        public async Task DeleteAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"DELETE FROM steps WHERE id = @Id",
                new { Id = id });
            
            await _dapperContext.Command(queryObject);
        }

        public async Task<DbStep> UpdateAsync(DbStep step)
        {
            var queryObject = new QueryObject(
                @"UPDATE steps SET approver_id = @ApproverId WHERE id = @Id
                RETURNING id as ""Id"", schema_id as ""SchemaId"", approver_id as ""ApproverId""",
                new
                {
                    step.Id,
                    step.ApproverId
                });
        
            return await _dapperContext.CommandWithResponse<DbStep>(queryObject);
        }
    }