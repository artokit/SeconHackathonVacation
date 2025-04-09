using Api.Exceptions.Abstractions;

namespace Api.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await ExceptionHandling(context, e);
        }
    }

    private async Task ExceptionHandling(HttpContext context, Exception e)
    {
        context.Response.StatusCode = e switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundRequestException => StatusCodes.Status404NotFound,
            ForbiddenRequestException => StatusCodes.Status403Forbidden,
            // _ => StatusCodes.Status500InternalServerError
        };

        await context.Response.WriteAsJsonAsync(new {error = e.Message});
    }
}