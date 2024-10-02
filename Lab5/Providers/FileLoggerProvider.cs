using Lab5.Logger;

namespace Lab5.Providers;

public class FileLoggerProvider : ILoggerProvider
{
    readonly string _path;
    
    public FileLoggerProvider(string path)
    {
        this._path = path;
    }
    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_path);
    }
 
    public void Dispose() {}
}

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
    {
        builder.AddProvider(new FileLoggerProvider(filePath));
        return builder;
    }
}