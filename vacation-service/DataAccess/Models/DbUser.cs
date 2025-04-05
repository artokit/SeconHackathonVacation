using Common;

namespace DataAccess.Models;

public class DbUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string HashedPassword { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
    public Guid? ImageId { get; set; }
    public Guid DepartmentId { get; set; }
    public string? Phone { get; set; }
    public string? TelegramUsername { get; set; }
}