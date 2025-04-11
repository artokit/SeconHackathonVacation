using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Schemas.Requests
{
    public class UpdateSchemaRequestDto
    {
        [Required(ErrorMessage = "Необходимо указать название схемы.")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Необходимо указать шаги согласования.")]
        [MinLength(1, ErrorMessage = "Должен быть хотя бы один шаг согласования.")]
        public List<Guid> Steps { get; set; } = new();
    }
}
