using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories;

public interface IUsersRepository
{
    public Task<DbUser> AddAsync(DbUser user);
    public Task<DbUser?> GetByEmailAsync(string email);
    public Task<DbUser?> GetByIdAsync(Guid userId);
    public Task DeleteAsync(Guid userId);
}