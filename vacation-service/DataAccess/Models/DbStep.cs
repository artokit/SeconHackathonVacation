namespace DataAccess.Models;

public class DbStep
{
    public Guid Id { get; set; }
    public Guid SchemaId { get; set; }
    public Guid ApproverId { get; set; }
}