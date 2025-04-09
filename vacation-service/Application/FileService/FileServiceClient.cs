using Application.Common.Interfaces;
using Application.FileService.Models;

namespace Application.FileService;

public class FileServiceClient : IFileServiceClient
{
    public async Task<Image?> GetImageById(Guid id)
    {
        return new Image
        {
            Id = id
        };
    }
}