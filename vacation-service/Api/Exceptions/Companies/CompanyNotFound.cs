using Api.Exceptions.Abstractions;

namespace Api.Exceptions.Companies
{
    public class CompanyNotFound : NotFoundException
    {
        public CompanyNotFound(string? message = "Компания не найдена") : base(message) { }
    }
}
