namespace Api.Exceptions.Abstractions;

public abstract class NotFoundRequestException : Exception
{
    protected NotFoundRequestException(string? message) : base(message) { }
}