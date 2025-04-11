using System.Net.Http.Json;
using Application.Common.Interfaces;
using Application.NotificationService.Models;
using Microsoft.Extensions.Configuration;

namespace Application.NotificationService;

public class NotificationServiceClient : INotificationServiceClient
{
    private HttpClient _httpClient;
    private readonly string _baseUrl;
    
    public NotificationServiceClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["Services:NotificationServiceUrl"] ?? throw new ArgumentException("NotificationServiceUrl is empty");
    }
    
    public async Task SendGeneratedPasswordAsync(GeneratedPasswordRequest generatedPassword)
    {
        var data = new Dictionary<string, string>
        {
            {"to_email", generatedPassword.ToEmail},
            {"password", generatedPassword.Password}
        };

        await _httpClient.PostAsJsonAsync($"{_baseUrl}/generated-password", data);
    }
}