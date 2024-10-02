namespace Lab5;
using MyMiddlewares;

public static class ApplicationBuilderExtensions
{
    public static void UseExceptionLoggerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionLoggerMiddleware>();
    }
}