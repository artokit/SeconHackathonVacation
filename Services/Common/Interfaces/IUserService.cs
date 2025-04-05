using Contracts.Authorization.Requests;
using Contracts.Employees.Responses;
using Contracts.Users.Requests;
using Contracts.Users.Responses;

namespace Services.Common.Interfaces;

public interface IUserService
{
    public Task<LoginSuccessResponse> RegisterAsync(RegisterRequestDto registerRequestDto);
    public Task<LoginSuccessResponse> LoginAsync(LoginRequestDto loginRequestDto);
    public Task<GetEmployeeResponseDto> GetMe(Guid userId);
}