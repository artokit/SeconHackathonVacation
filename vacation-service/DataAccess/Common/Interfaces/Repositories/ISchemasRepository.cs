using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories;

public interface ISchemasRepository
{
    public Task<DbSchema> AddAsync(DbSchema dbSchema);
    public Task<DbSchema?> GetByIdAsync(Guid id);
    public Task DeleteAsync(Guid id);
    public Task<DbSchema> UpdateAsync(DbSchema schema);
}