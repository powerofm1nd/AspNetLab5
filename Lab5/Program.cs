using Lab5;
using Lab5.Services;
using ILogger = Lab5.Services.ILogger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ILogger, TextLoggerService>();

var app = builder.Build();

//Варіант 1 перехват та запис Exception через власний Middleware
app.UseExceptionLoggerMiddleware();
//Варіант 2 перехват та запис Exception через UseExceptionHandler
/*app.UseExceptionHandler(app => app.Run(async (HttpContext context) =>
{
    context.Response.StatusCode = 500;
    
    Exception exception = context.Features.Get<IExceptionHandlerFeature>().Error;
    ILogger logger = context.RequestServices.GetService<ILogger>();
    
    logger.WriteLine(exception.Message);
    
    await context.Response.WriteAsync($"Exception occured: {exception.Message}");
}));*/

app.MapGet("/", (HttpResponse  response) =>
{
    //throw new Exception("123"); //Генерація помилки
    var html = File.ReadAllText(@"./www/MyForm.html");
    response.WriteAsync(html);
});

app.MapGet("/cookies", (HttpRequest request, HttpResponse  response) =>
{
    var html = "<html><head></head><body><ul>";
    
    foreach (var cookie in request.Cookies)
    {
        html += "<li>" + cookie.Key + ": " + cookie.Value + "</li>";    
    }
    
    html += "</ul></body>";
    
    response.WriteAsync(html);
});

app.MapPost("/test/",  string (HttpRequest request, HttpResponse response) =>
{
    string cookieKey = "test-cookie";
    string cookieValue = request.Form["myValue"];
    DateTime expiresDateTime = DateTime.Parse(request.Form["myTime"]);
    CookieOptions option = new CookieOptions { Expires = expiresDateTime };
    
    if (!request.Cookies.ContainsKey(request.Form["myValue"])) 
    {
        response.Cookies.Delete(request.Form["myValue"]);
    }
    
    response.Cookies.Append(cookieKey, cookieValue, option);
    
    return "OK";
});

app.Run();