using Api.Dto.Authorization.Requests;
using Api.Dto.Authorization.Responses;
using Api.Dto.Employees.Responses;

namespace Api.Services.Interfaces;

public interface IUserService
{
    public Task<LoginSuccessResponse> RegisterAsync(RegisterRequestDto registerRequestDto);
    public Task<LoginSuccessResponse> LoginAsync(LoginRequestDto loginRequestDto);
    public Task<GetEmployeeResponseDto> GetMe(Guid userId);
}