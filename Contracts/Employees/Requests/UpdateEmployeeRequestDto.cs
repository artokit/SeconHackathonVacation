using Common;

namespace Contracts.Employees.Requests;

public class UpdateEmployeeRequestDto
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