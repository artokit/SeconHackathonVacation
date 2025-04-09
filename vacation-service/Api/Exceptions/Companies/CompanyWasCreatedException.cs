using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Companies;

public class CompanyWasCreatedException : BadRequestException
{
    public CompanyWasCreatedException(string? message = "Компания уже была создана") : base(message) { }
}