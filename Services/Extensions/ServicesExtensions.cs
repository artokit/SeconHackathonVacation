using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;
using Services.Users;

namespace Services.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}