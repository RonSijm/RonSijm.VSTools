namespace RonSijm.VSTools.Core.Logging.Features.Dispatching;

public interface IAsyncLogger
{
    Task Log(LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args);
}

// ReSharper disable once UnusedTypeParameter
public interface IAsyncLogger<T> : IAsyncLogger;