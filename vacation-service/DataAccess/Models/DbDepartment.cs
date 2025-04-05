namespace DataAccess.Models;

public class DbDepartment
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid SupervisorId { get; set; }
}