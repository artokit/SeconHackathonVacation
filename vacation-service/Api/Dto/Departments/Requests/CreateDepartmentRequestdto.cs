using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Departments.Requests;

public class CreateDepartmentRequestdto
{
    [Required(ErrorMessage = "Название отдела обязательно для заполнения.")]
    [MinLength(1, ErrorMessage = "Название отдела не может быть пустым.")]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Необходимо назначить руководителя отдела.")]
    public Guid SupervisorId { get; set; }
}