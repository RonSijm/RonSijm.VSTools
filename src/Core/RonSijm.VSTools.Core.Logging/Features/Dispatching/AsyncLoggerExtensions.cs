namespace RonSijm.VSTools.Core.Logging.Features.Dispatching;

public static class AsyncLoggerExtensions
{
    public static async Task LogDebug(this IAsyncLogger logger, EventId eventId, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Debug, eventId, exception, message, args);
    }

    public static async Task LogDebug(this IAsyncLogger logger, EventId eventId, string message, params object[] args)
    {
        await logger.Log(LogLevel.Debug, eventId, message, args);
    }

    public static async Task LogDebug(this IAsyncLogger logger, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Debug, exception, message, args);
    }

    public static async Task LogDebug(this IAsyncLogger logger, string message, params object[] args)
    {
        await logger.Log(LogLevel.Debug, message, args);
    }

    public static async Task LogTrace(this IAsyncLogger logger, EventId eventId, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Trace, eventId, exception, message, args);
    }

    public static async Task LogTrace(this IAsyncLogger logger, EventId eventId, string message, params object[] args)
    {
        await logger.Log(LogLevel.Trace, eventId, message, args);
    }

    public static async Task LogTrace(this IAsyncLogger logger, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Trace, exception, message, args);
    }

    public static async Task LogTrace(this IAsyncLogger logger, string message, params object[] args)
    {
        await logger.Log(LogLevel.Trace, message, args);
    }

    public static async Task LogInformation(this IAsyncLogger logger, EventId eventId, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Information, eventId, exception, message, args);
    }

    public static async Task LogInformation(this IAsyncLogger logger, EventId eventId, string message, params object[] args)
    {
        await logger.Log(LogLevel.Information, eventId, message, args);
    }

    public static async Task LogInformation(this IAsyncLogger logger, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Information, exception, message, args);
    }

    public static async Task LogInformation(this IAsyncLogger logger, string message, params object[] args)
    {
        await logger.Log(LogLevel.Information, message, args);
    }

    public static async Task LogWarning(this IAsyncLogger logger, EventId eventId, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Warning, eventId, exception, message, args);
    }

    public static async Task LogWarning(this IAsyncLogger logger, EventId eventId, string message, params object[] args)
    {
        await logger.Log(LogLevel.Warning, eventId, message, args);
    }

    public static async Task LogWarning(this IAsyncLogger logger, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Warning, exception, message, args);
    }

    public static async Task LogWarning(this IAsyncLogger logger, string message, params object[] args)
    {
        await logger.Log(LogLevel.Warning, message, args);
    }

    public static async Task LogError(this IAsyncLogger logger, EventId eventId, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Error, eventId, exception, message, args);
    }

    public static async Task LogError(this IAsyncLogger logger, EventId eventId, string message, params object[] args)
    {
        await logger.Log(LogLevel.Error, eventId, message, args);
    }

    public static async Task LogError(this IAsyncLogger logger, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Error, exception, message, args);
    }

    public static async Task LogError(this IAsyncLogger logger, string message, params object[] args)
    {
        await logger.Log(LogLevel.Error, message, args);
    }

    public static async Task LogCritical(this IAsyncLogger logger, EventId eventId, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Critical, eventId, exception, message, args);
    }

    public static async Task LogCritical(this IAsyncLogger logger, EventId eventId, string message, params object[] args)
    {
        await logger.Log(LogLevel.Critical, eventId, message, args);
    }

    public static async Task LogCritical(this IAsyncLogger logger, Exception exception, string message, params object[] args)
    {
        await logger.Log(LogLevel.Critical, exception, message, args);
    }

    public static async Task LogCritical(this IAsyncLogger logger, string message, params object[] args)
    {
        await logger.Log(LogLevel.Critical, message, args);
    }

    public static async Task Log(this IAsyncLogger logger, LogLevel logLevel, string message, params object[] args)
    {
        if (logger == null)
        {
            return;
        }

        await logger.Log(logLevel, 0, null, message, args);
    }

    public static async Task Log(this IAsyncLogger logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
    {
        await logger.Log(logLevel, eventId, null, message, args);
    }

    public static async Task Log(this IAsyncLogger logger, LogLevel logLevel, Exception exception, string message, params object[] args)
    {
        await logger.Log(logLevel, 0, exception, message, args);
    }
}