namespace SPG.EventSourcing.Logger
{
    public interface ILogger
    {
        void Log(string message, LogSeverity severity);
    }
}