using System.Reflection;
using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Dapper.Settings;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Dapper.Settings;
using DataAccess.Repositories;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class DataAccessExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IDapperSettings, DapperSettings>();
        services.AddScoped<IDapperContext, DapperContext>();
        
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
        services.AddScoped<ICompaniesRepository, CompaniesRepository>();
        services.AddScoped<ISchemasRepository, SchemasRepository>();
        services.AddScoped<IStepsRepository, StepsRepository>();
        return services;
    }
    
    public static IServiceCollection AddMigrations(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddLogging(c => c.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(c => c
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("Database"))
                .ScanIn(Assembly.GetExecutingAssembly()).For.All());
        
        return services;
    }

    public static IServiceProvider UseMigrations(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
        runner.MigrateUp();
        
        return serviceProvider;
    }
}