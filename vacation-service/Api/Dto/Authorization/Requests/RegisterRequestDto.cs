using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Authorization.Requests;

public class RegisterRequestDto
{
    [Required]
    [MinLength(1, ErrorMessage = "Имя не может быть пустым")]
    public string Name { get; set; }
    
    [Required]
    [MinLength(1, ErrorMessage = "Фамилия не может быть пустым")]
    public string Surname { get; set; }
    
    [MinLength(1, ErrorMessage = "Отчество не может быть пустым")]
    public string? Patronymic { get; set; }
    
    [MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов")]
    public string Password { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Неверный формат email")]
    public string Email { get; set; }
}