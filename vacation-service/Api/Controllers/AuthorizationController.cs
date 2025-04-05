using Api.Dto.Authorization.Requests;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IUserService = Api.Services.Interfaces.IUserService;

namespace Api.Controllers;


[ApiController]
[Route("vacation-service/auth")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    
    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
    {
        return Ok(await _authorizationService.RegisterAsync(registerRequestDto));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        return Ok(await _authorizationService.LoginAsync(loginRequestDto));
    }
}