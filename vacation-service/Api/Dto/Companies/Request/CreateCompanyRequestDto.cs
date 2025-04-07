using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Companies.Request
{
    public class CreateCompanyRequestDto
    {
        [Required(ErrorMessage = "Название компании обязательно для заполнения.")]
        [MinLength(1, ErrorMessage = "Название компании не может быть пустым.")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Необходимо назначить руководителя компании.")]
        public Guid SupervisorId { get; set; }
    }
}

