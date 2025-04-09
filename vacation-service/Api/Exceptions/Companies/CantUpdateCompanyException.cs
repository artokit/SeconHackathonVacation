using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Companies;

public class CantUpdateCompanyException : ForbiddenRequestException
{
    public CantUpdateCompanyException(string? message = "Вы не можете редактировать данную компанию") : base(message) { }
}