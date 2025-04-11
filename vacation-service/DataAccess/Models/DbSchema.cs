namespace DataAccess.Models;

public class DbSchema
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
}