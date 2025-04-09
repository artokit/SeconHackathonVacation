namespace Api.Exceptions.Abstractions;

public abstract class ForbiddenRequestException : Exception
{
    protected ForbiddenRequestException(string? message) : base(message) { }
}