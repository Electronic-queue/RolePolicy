using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

public class CorrelationIdMiddleware : IMiddleware
{
    private readonly IProblemDetailsService _problemDetailsService;
    private string correlationId;

    public CorrelationIdMiddleware(IProblemDetailsService problemDetailsService)
    {
        ArgumentNullException.ThrowIfNull(problemDetailsService);
        _problemDetailsService = problemDetailsService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Headers.TryGetValue("X-Correlation-Id", out var values))
        {
            correlationId = values.First()!;
            if (correlationId.Length > 128)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "CorrelationId exceeded max length of 128 chars",
                    Instance = context.Request.Path
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(problemDetails);
                return;
            }

            context.TraceIdentifier = correlationId;
        }
        else
        {
            correlationId = Guid.NewGuid().ToString();
            context.Request.Headers.Append("X-Correlation-Id", correlationId);
        }
        var activityFeature = context.Features.GetRequiredFeature<IHttpActivityFeature>();
        var activity = activityFeature.Activity;
        activity.AddTag("correlationId", context.TraceIdentifier);
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await next(context);
        }
    }
}
