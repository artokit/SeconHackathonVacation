using Api.Services;
using Api.Services.Interfaces;

namespace Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<ICompanyService, CompanyService>();
        return services;
    }
}