using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common;
using Contracts.Authorization.Requests;
using Contracts.Employees.Responses;
using Contracts.Users.Requests;
using Contracts.Users.Responses;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Common.Interfaces;
using Services.Common.Settings;
using Services.Exceptions.Users;
using Services.Mappers;
using ClaimTypes = System.Security.Claims.ClaimTypes;
using CustomClaimTypes = Common.ClaimTypes;

namespace Services.Users;

public class UserService : IUserService
{
    private IUsersRepository _usersRepository;
    private readonly JwtSettings _jwtSettings;
    
    public UserService(IOptions<JwtSettings> jwtSettings, IUsersRepository usersRepository)
    {
        _jwtSettings = jwtSettings.Value;
        _usersRepository = usersRepository;
    }

    public async Task<LoginSuccessResponse> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        if (await _usersRepository.GetByEmailAsync(registerRequestDto.Email) != null)
        {
            throw new EmailIsExistException();
        }
        
        var dbUser = registerRequestDto.MapToDb();
        dbUser.Role = Roles.Director;
        
        var res = await _usersRepository.AddAsync(dbUser);
        return new LoginSuccessResponse { AccessToken = GenerateAccessToken(res) };
    }

    public async Task<LoginSuccessResponse> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var res = await _usersRepository.GetByEmailAsync(loginRequestDto.Email);
        
        if (res is null)
        {
            throw new FailureAuthorizationException();
        }

        if (!PasswordService.Verify(loginRequestDto.Password, res.HashedPassword))
        {
            throw new FailureAuthorizationException();
        }

        return new LoginSuccessResponse { AccessToken = GenerateAccessToken(res) };
    }

    public async Task<GetEmployeeResponseDto> GetMe(Guid userId)
    {
        var res = await _usersRepository.GetByIdAsync(userId);
        
        if (res is null)
        {
            throw new UserNotFound();
        }

        return res.MapToDto();
    }

    private string GenerateAccessToken(DbUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = _jwtSettings.GetSymmetricSecurityKey();

        var claims = new[]
        {
            new Claim(CustomClaimTypes.Id.ToString(), user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryInMinutes),
            signingCredentials: new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256)
        );

        return tokenHandler.WriteToken(token);
    }
}