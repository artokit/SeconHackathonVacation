using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Companies
{
    public class CompanyNotFoundException : NotFoundRequestException
    {
        public CompanyNotFoundException(string? message = "Компания не найдена") : base(message) { }
    }
}
