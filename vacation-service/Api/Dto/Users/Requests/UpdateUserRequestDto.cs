using System.ComponentModel.DataAnnotations;
using Common;

namespace Api.Dto.Users.Requests;

public class UpdateUserRequestDto
{
    [Required(ErrorMessage = "Укажите какого сотрудника вы хотите поменять")]
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public Guid? DepartmentId { get; set; }
    public string? Phone { get; set; }
    public string? TelegramUsername { get; set; }
    public Guid? ImageId { get; set; }
}