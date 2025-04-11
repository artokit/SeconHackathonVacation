using Api.Dto.Users.Requests;
using Api.Dto.Users.Responses;

namespace Api.Services.Interfaces;

public interface IUserService
{
    public Task<GetUserResponseDto> GetMe(Guid userId);
    public Task<GetUserResponseDto> CreateAsync(Guid userId, CreateUserRequestDto request);
    public Task DeleteAsync(Guid userId, Guid employeeId);
    public Task<GetUserResponseDto> UpdateAsync(Guid userId, UpdateUserRequestDto request);
}