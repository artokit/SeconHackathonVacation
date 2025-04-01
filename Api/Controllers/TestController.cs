using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok("test");
    }
}