using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories;

public interface IStepsRepository
{
    public Task<DbStep> AddAsync(DbStep dbStep);
    public Task<DbStep?> GetByIdAsync(Guid id);
    public Task<List<DbStep>> GetBySchemaIdAsync(Guid schemaId);
    public Task DeleteAsync(Guid id);
    public Task<DbStep> UpdateAsync(DbStep step);
}