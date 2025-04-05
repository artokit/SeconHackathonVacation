using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Abstractions;

[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{
    private string AuthHeader => HttpContext.Request.Headers.Authorization.ToString();

    protected Guid UserId => AuthHeader.GetUserId();
}