namespace Api.Dto.Companies.Responses
{
    public class GetCompanyResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid DirectorId { get; set; }
    }
}