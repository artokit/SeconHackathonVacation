using Common;

namespace Api.Dto.Users.Requests;

public class UpdateUserRequestDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Email { get; set; }
    public Guid? DepartmentId { get; set; }
    public Roles? Role { get; set; }
    public string? Phone { get; set; }
    public string? TelegramUsername { get; set; }
    public string? ImageId { get; set; }
}