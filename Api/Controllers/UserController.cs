using Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Interfaces;

namespace Api.Controllers;

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