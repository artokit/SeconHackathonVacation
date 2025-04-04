using Common;

namespace Contracts.Users.Responses;

public class GetMeResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? ImageId { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
}