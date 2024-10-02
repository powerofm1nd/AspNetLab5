namespace Lab5.Services;

public class TextLoggerService : ILogger, IDisposable
{
    private readonly object _lock;
    
    private readonly FileStream _fileStream;
    private readonly StreamWriter _streamWriter;
    
    public TextLoggerService(IConfiguration configuration)
    {
        var logFilePath = Path.GetFullPath(configuration["FileLoggerPath"]);
        _lock = new object();
        _fileStream = File.Open(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read);
        _streamWriter = new StreamWriter(_fileStream);
    }
    
    public void WriteLine(string message)
    {
        lock (_lock)
        {
            _streamWriter.WriteLine($"{DateTime.Now:MM/dd/yyyy h:mm:ss tt} Exception.Message: {message}");
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            _streamWriter?.Dispose();
            _fileStream?.Dispose();
        }
    }
}