using Microsoft.Extensions.DependencyInjection;
using Services.Common.Interfaces;
using Services.Employees;
using Services.Users;

namespace Services.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmployeesService, EmployeesService>();
        return services;
    }
}