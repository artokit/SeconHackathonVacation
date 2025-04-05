using Api.Dto.Authorization.Requests;
using Api.Dto.Authorization.Responses;

namespace Api.Services.Interfaces;

public interface IAuthorizationService
{
    public Task<LoginSuccessResponse> RegisterAsync(RegisterRequestDto registerRequestDto);
    public Task<LoginSuccessResponse> LoginAsync(LoginRequestDto loginRequestDto);
}