using Api.Dto.Companies.Responses;

namespace Api.Dto.Users.Responses;

public class GetMeResponseDto : GetUserResponseDto
{
    public GetCompanyResponseDto? Company { get; set; }
}