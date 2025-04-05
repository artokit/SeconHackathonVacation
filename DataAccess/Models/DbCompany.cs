namespace DataAccess.Models;

public class DbCompany
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Supervisor_Id { get; set; }
}