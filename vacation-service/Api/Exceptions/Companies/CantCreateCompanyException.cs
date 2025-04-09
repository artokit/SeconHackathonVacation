using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Companies;

public class CantCreateCompanyException : ForbiddenRequestException
{
    public CantCreateCompanyException(string? message = "Вы не можете создавать компанию") : base(message) { }
}