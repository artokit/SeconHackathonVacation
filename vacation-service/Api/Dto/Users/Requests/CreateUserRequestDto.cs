using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.Dto.Users.Requests;

public class CreateUserRequestDto
{
    [Required]
    [MinLength(1, ErrorMessage = "Имя не может быть пустым")]
    public string Name { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Фамилия не может быть пустой")]
    public string Surname { get; set; }
    
    [MinLength(1, ErrorMessage = "Отчество не может быть пустым")]
    public string? Patronymic { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Неверный формат email")]
    public string Email { get; set; }
    
    [Required]
    public Guid DepartmentId { get; set; }
    
    [Required]
    public Roles Role { get; set; }
}