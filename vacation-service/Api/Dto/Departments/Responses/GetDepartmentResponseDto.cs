namespace Api.Dto.Departments.Responses;

public class GetDepartmentResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid SupervisorId { get; set; }
}