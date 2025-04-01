using System.ComponentModel.DataAnnotations;

namespace Contracts.Users.Requests;

public class LoginRequestDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Неверный формат email")]
    public string Email { get; set; }
    
    [MinLength(6, ErrorMessage = "Пароль не может быть короче 6 символов")]
    public string Password { get; set; }
}