using Common;

namespace Contracts.Employees.Responses;

public class GetEmployeeResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string Email { get; set; }
    public Guid DepartmentId { get; set; }
    public Roles Role { get; set; }
    public string? Phone { get; set; }
    public string? TelegramUsername { get; set; }
    public Guid? ImageId { get; set; }
}