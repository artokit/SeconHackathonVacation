using Application.NotificationService.Models;

namespace Application.Common.Interfaces;

public interface INotificationServiceClient
{
    public Task SendGeneratedPasswordAsync(GeneratedPasswordRequest generatedPassword);
}