using Application.Common.Interfaces;
using Application.NotificationService;
using Application.FileService;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<INotificationServiceClient, NotificationServiceClient>();
        services.AddScoped<IFileServiceClient, FileServiceClient>();
        return services;
    }
}