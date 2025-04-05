using Api.Services;
using Api.Services.Interfaces;

namespace Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        return services;
    }
}