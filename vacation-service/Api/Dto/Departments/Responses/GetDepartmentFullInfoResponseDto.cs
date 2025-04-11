using Api.Dto.Users.Responses;

namespace Api.Dto.Departments.Responses;

public class GetDepartmentFullInfoResponseDto : GetDepartmentResponseDto
{
    public List<GetUserResponseDto> Employees { get; set; }
}