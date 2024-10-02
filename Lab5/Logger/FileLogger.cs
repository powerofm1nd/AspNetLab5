namespace Lab5.Logger;
public class FileLogger : ILogger, IDisposable
{
    private string _filePath;
    static object _locker = new object();
    /*
    FileStream _fileStream;
    StreamWriter _streamWriter;
    */
    
    public FileLogger(string path) {
        _filePath = path;
        /*_fileStream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Write);
        _streamWriter = new StreamWriter(_fileStream);*/
    }
    
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }
    
    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_locker)
        {
            /*_streamWriter.WriteLine($"{DateTime.Now:MM/dd/yyyy h:mm:ss tt} {formatter(state, exception)}");*/
            File.AppendAllText(_filePath, $"{DateTime.Now:MM/dd/yyyy h:mm:ss tt} {formatter(state, exception)}\n");
        }
    }

    public void Dispose()
    {
        /*lock (_locker)
        {
            _streamWriter.Dispose();
            _fileStream.Dispose();
        }*/
    }
}