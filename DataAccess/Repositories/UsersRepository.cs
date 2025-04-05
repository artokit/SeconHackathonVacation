using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private IDapperContext _dapperContext;
    
    public UsersRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<DbUser> AddAsync(DbUser dbUser)
    {
        var queryObject = new QueryObject(
            @"INSERT INTO users(name, surname, patronymic, hashed_password, email, role) 
            VALUES(@Name, @Surname, @Patronymic, @HashedPassword, @Email, @role) 
            RETURNING id as ""Id"", name as ""Name"", surname as ""Surname"", image_id as ""ImageId"", patronymic as ""Patronymic"", email as ""Email"", role as ""Role"", hashed_password as ""HashedPassword"", telegram_username as ""TelegramUsername"", phone as ""Phone""",
            new
            {
                dbUser.Name,
                dbUser.Surname,
                dbUser.Patronymic,
                HashedPassword = dbUser.HashedPassword,
                dbUser.Email,
                role = dbUser.Role
            });
        
        var res = await _dapperContext.CommandWithResponse<DbUser>(queryObject);
        return res;
    }

    public async Task<DbUser?> GetByEmailAsync(string email)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", patronymic as ""Patronymic"", email as ""Email"", role as ""Role"", hashed_password as ""HashedPassword"" FROM USERS 
                 WHERE email=@email",
            new { email });
        
        return await _dapperContext.FirstOrDefault<DbUser>(queryObject);
    }

    public async Task<DbUser?> GetByIdAsync(Guid userId)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", surname as ""Surname"", image_id as ""ImageId"", patronymic as ""Patronymic"", email as ""Email"", role as ""Role"", hashed_password as ""HashedPassword"", telegram_username as ""TelegramUsername"", phone as ""Phone"" FROM USERS WHERE id=@id",
            new
            {
                id = userId
            });
        
        return await _dapperContext.FirstOrDefault<DbUser>(queryObject);
    }

    public async Task DeleteAsync(Guid userId)
    {
        var queryObject = new QueryObject(
            @"DELETE FROM USERS WHERE id=@id",
            new { id = userId });
        
        await _dapperContext.Command(queryObject);
    }
}