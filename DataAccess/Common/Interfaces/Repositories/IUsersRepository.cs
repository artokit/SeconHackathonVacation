using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories;

public interface IUsersRepository
{
    public Task<DbUser> CreateAsync(DbUser user);
    public Task<DbUser?> GetUserByEmailAsync(string email);
}