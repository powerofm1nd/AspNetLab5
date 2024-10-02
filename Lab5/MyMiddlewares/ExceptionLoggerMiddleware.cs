namespace Lab5.MyMiddlewares;
using Services;

public class ExceptionLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    
    public ExceptionLoggerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
        catch (Exception e)
        {
            //Запис помилки
            _logger.WriteLine(e.Message);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync($"An unexpected error occurred. The exception has logged into the file. Ex: {e.Message}");
        }

    }
}