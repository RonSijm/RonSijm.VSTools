using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.Module.NamespaceFixer.Core.Helpers;

public static class IndentedLoggerHelper
{
    private static readonly Dictionary<int, string> IndentationCache = new();

    public static async Task LogInformationIndented<T>(this IAsyncLogger<T> logger, int indentationLevel, string message, params object[] args)
    {
        await LogIndented(logger, LogLevel.Information, indentationLevel, message, args);
    }

    public static async Task LogWarningIndented<T>(this IAsyncLogger<T> logger, int indentationLevel, string message, params object[] args)
    {
        await LogIndented(logger, LogLevel.Warning, indentationLevel, message, args);
    }

    public static async Task LogErrorIndented<T>(this IAsyncLogger<T> logger, int indentationLevel, string message, params object[] args)
    {
        await LogIndented(logger, LogLevel.Error, indentationLevel, message, args);
    }

    public static async Task LogIndented<T>(this IAsyncLogger<T> logger, LogLevel logLevel, int indentationLevel, string message, params object[] args)
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

            await logger.Log(logLevel, indentedMessage, newArgs);
        }
        else
        {
            // If message is null or empty, simply log it without indentation
            await logger.Log(logLevel, message, args);
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