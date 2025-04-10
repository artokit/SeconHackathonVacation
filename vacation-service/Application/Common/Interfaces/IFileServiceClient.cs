using Application.FileService.Models;

namespace Application.Common.Interfaces;

public interface IFileServiceClient
{
    public Task<Image?> GetImageByIdAsync(Guid id);
}