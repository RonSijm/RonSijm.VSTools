using Microsoft.Extensions.Logging;

namespace RonSijm.VSTools.CLI.Transit;

public class LogMessage
{
    public LogMessage(LogLevel logLevel, EventId eventId, Exception exception, string message, object[] args)
    {
        Args = args;
        LogLevel = logLevel;
        EventId = eventId;
        Exception = exception;
        Message = message;
        Received = DateTime.Now;
        FormattedMessage = new FormattedLogValues(Message, args).ToString();
    }

    public DateTime Received { get; set; }

    public object[] Args { get; set; }
    public LogLevel LogLevel { get; set; }
    public EventId EventId { get; set; }
    public Exception Exception { get; set; }
    public string Message { get; set; }

    public string FormattedMessage { get; set; }
}