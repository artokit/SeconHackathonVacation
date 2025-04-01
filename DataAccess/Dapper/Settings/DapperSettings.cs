using DataAccess.Interfaces.Settings;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Dapper.Settings;

public class DapperSettings : IDapperSettings
{
    public DapperSettings(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("Database");
    }

    public string ConnectionString { get; set; }
}