using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Images;

public class ImageNotFoundException : BadRequestException
{
    public ImageNotFoundException(string? message = "Изображение не найдено") : base(message) { }
}