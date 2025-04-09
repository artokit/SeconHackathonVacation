using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Companies.Request
{
    public class UpdateCompanyRequestDto
    {
        [MinLength(1, ErrorMessage = "Название компании не может быть пустым.")]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
