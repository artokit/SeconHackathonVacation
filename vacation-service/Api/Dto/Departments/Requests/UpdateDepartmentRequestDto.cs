using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Departments.Requests;

public class UpdateDepartmentRequestDto
{
    [MinLength(1, ErrorMessage = "Название отдела не может быть пустым.")]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Необходимо назначить руководителя отдела.")]
    public Guid SupervisorId { get; set; }
    public Guid? ImageId { get; set; }
}