using System.ComponentModel.DataAnnotations;

namespace Application.NotificationService.Models;

public class GeneratedPasswordRequest
{
    [Required]
    [EmailAddress]
    public string ToEmail { get; set; }
    
    [Required]
    [MaxLength(6)]
    public string Password { get; set; }
}