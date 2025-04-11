using Api.Services;
using Api.Services.Common;
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
        services.AddScoped<IRightService, RightService>();
        return services;
    }
}