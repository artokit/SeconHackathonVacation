using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("test1")]
    public async Task<IActionResult> Test()
    {
        return Ok("test");
    }
}