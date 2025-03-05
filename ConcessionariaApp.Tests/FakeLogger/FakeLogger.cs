using Microsoft.Extensions.Logging;

public class FakeLogger<T> : ILogger<T>
{
    public List<string> Logs { get; } = new List<string>();

    public IDisposable BeginScope<TState>(TState state) => NullDisposable.Instance;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        var message = formatter(state, exception);
        Logs.Add(message);
    }
}

public class NullDisposable : IDisposable
{
    public static readonly NullDisposable Instance = new NullDisposable();

    public void Dispose() { }
}
