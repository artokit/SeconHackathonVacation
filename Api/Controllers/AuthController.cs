using Contracts.Users.Requests;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;


[ApiController]
public class AuthController : ControllerBase
{
    private IUserService _userService;
    
    public AuthController(IUserService userService)
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