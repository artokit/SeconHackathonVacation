using Contracts.Users.Requests;
using Contracts.Users.Responses;

namespace Services.Interfaces;

public interface IUserService
{
    public Task<LoginSuccessResponse> RegisterAsync(RegisterRequestDto registerRequestDto);
    public Task<LoginSuccessResponse> LoginAsync(LoginRequestDto loginRequestDto);
}