using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace RolePolicy.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next) =>
        _next = next;
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch {
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            var result = JsonSerializer.Serialize( new{
                error = exception.Message,
                statusCode = context.Response.StatusCode
            });
            await context.Response.WriteAsync(result);
        }
    }
}
