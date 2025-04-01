using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("test")]
public class TestController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Test()
    {
        return Ok("ok");
    }
}