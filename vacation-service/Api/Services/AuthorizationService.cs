using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Dto.Authorization.Requests;
using Api.Dto.Authorization.Responses;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using Common;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = System.Security.Claims.ClaimTypes;
using CustomClaimTypes = Common.ClaimTypes;

namespace Api.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUsersRepository _usersRepository;
    private readonly JwtSettings _jwtSettings;
    
    public AuthorizationService(IUsersRepository usersRepository, IOptions<JwtSettings> jwtSettings)
    {
        _usersRepository = usersRepository;
        _jwtSettings = jwtSettings.Value;
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
            throw new FailureAuthorizationRequestException();
        }

        if (!PasswordService.Verify(loginRequestDto.Password, res.HashedPassword))
        {
            throw new FailureAuthorizationRequestException();
        }

        return new LoginSuccessResponse { AccessToken = GenerateAccessToken(res) };
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