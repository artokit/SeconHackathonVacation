using Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using IUserService = Api.Services.Interfaces.IUserService;

namespace Api.Controllers;

[Route("vacation-service/users")]
public class UserController : BaseController
{
    private IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        return Ok(await _userService.GetMe(UserId));
    }
}