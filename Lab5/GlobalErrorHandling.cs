using System.Net;

namespace Lab5;

public class GlobalErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandling> _logger;

    public GlobalErrorHandling(RequestDelegate next, ILogger<GlobalErrorHandling> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await context.Response.WriteAsJsonAsync(new { StatusCode = context.Response.StatusCode, Errors = exception.ToString() });
        _logger.LogError(exception.ToString());
    }
}