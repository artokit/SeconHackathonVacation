using System.Net;
using System.Text.Json;
using Application.Common.Interfaces;
using Application.FileService.Models;
using Microsoft.Extensions.Configuration;

namespace Application.FileService;

public class FileServiceClient : IFileServiceClient
{
    private HttpClient _httpClient;
    private readonly string _baseUrl;

    public FileServiceClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["Services:FileServiceUrl"];
    }

    public async Task<Image?> GetImageByIdAsync(Guid id)
    {
        var res = await _httpClient.GetAsync($"{_baseUrl}/{id}");
        
        if (res.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        var content = await res.Content.ReadAsStringAsync();
        var image = JsonSerializer.Deserialize<Image>(content);
        Console.WriteLine(image.Id);
        return image;
    }
}