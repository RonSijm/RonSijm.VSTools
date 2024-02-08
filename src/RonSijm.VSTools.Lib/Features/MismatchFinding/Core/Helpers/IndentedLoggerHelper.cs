namespace RonSijm.VSTools.Lib.Features.MismatchFinding.Core.Helpers;

public static class IndentedLoggerHelper
{
    private static readonly Dictionary<int, string> IndentationCache = new();

    public static void LogInformationIndented<T>(this ILogger<T> logger, int indentationLevel, string message, params object[] args)
    {
        LogIndented(logger, LogLevel.Information, indentationLevel, message, args);
    }

    public static void LogWarningIndented<T>(this ILogger<T> logger, int indentationLevel, string message, params object[] args)
    {
        LogIndented(logger, LogLevel.Warning, indentationLevel, message, args);
    }

    public static void LogErrorIndented<T>(this ILogger<T> logger, int indentationLevel, string message, params object[] args)
    {
        LogIndented(logger, LogLevel.Error, indentationLevel, message, args);
    }

    public static void LogIndented<T>(this ILogger<T> logger, LogLevel logLevel, int indentationLevel, string message, params object[] args)
    {
        if (!string.IsNullOrEmpty(message))
        {
            var indentation = GetIndentationString(indentationLevel);
            var indentedMessage = "{indentation}" + message;

            object[] newArgs;
            if (args == null || args.Length == 0)
            {
                newArgs = [indentation];
            }
            else
            {
                newArgs = new object[args.Length + 1];
                newArgs[0] = indentation;
                Array.Copy(args, 0, newArgs, 1, args.Length);
            }

            logger.Log(logLevel, indentedMessage, newArgs);
        }
        else
        {
            // If message is null or empty, simply log it without indentation
            logger.Log(logLevel, message, args);
        }
    }

    private static string GetIndentationString(int indentationLevel)
    {
        if (!IndentationCache.ContainsKey(indentationLevel))
        {
            IndentationCache[indentationLevel] = new string(' ', indentationLevel * 4);
        }
        return IndentationCache[indentationLevel];
    }
}