using Api.Controllers.Abstractions;
using Api.Dto.Users.Requests;
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
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRequestDto request)
    {
        return Ok(await _userService.CreateAsync(UserId, request));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid employeeId)
    {
        await _userService.DeleteAsync(UserId, employeeId);
        return NoContent();
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> Update(Guid userId, [FromBody] UpdateUserRequestDto updateRequestDto)
    {
        return Ok(await _userService.UpdateAsync(UserId, userId, updateRequestDto));
    }
}