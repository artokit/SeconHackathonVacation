using Api.Dto.Authorization.Requests;
using Microsoft.AspNetCore.Mvc;
using IUserService = Api.Services.Interfaces.IUserService;

namespace Api.Controllers;


[ApiController]
[Route("vacation-service")]
public class AuthorizationController : ControllerBase
{
    private IUserService _userService;
    
    public AuthorizationController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
    {
        return Ok(await _userService.RegisterAsync(registerRequestDto));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        return Ok(await _userService.LoginAsync(loginRequestDto));
    }
}