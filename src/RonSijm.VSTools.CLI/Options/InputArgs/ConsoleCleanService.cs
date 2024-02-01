using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace RonSijm.VSTools.CLI.Options.InputArgs;

public static class ConsoleCleanService
{
    public static void CleanConsole(this ILogger logger)
    {
        Console.Clear();
        logger.LogTrace("");
        logger.LogTrace("██████╗  ██████╗ ███╗   ██╗███████╗██╗     ██╗███╗   ███╗       ");
        logger.LogTrace("██╔══██╗██╔═══██╗████╗  ██║██╔════╝██║     ██║████╗ ████║       ");
        logger.LogTrace("██████╔╝██║   ██║██╔██╗ ██║███████╗██║     ██║██╔████╔██║       ");
        logger.LogTrace("██╔══██╗██║   ██║██║╚██╗██║╚════██║██║██   ██║██║╚██╔╝██║       ");
        logger.LogTrace("██║  ██║╚██████╔╝██║ ╚████║███████║██║╚█████╔╝██║ ╚═╝ ██║    ██╗");
        logger.LogTrace("╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝╚═╝ ╚════╝ ╚═╝     ╚═╝    ╚═╝");
        logger.LogTrace("                                                                ");
        logger.LogTrace("██╗   ██╗███████╗████████╗ ██████╗  ██████╗ ██╗     ███████╗    ");
        logger.LogTrace("██║   ██║██╔════╝╚══██╔══╝██╔═══██╗██╔═══██╗██║     ██╔════╝    ");
        logger.LogTrace("██║   ██║███████╗   ██║   ██║   ██║██║   ██║██║     ███████╗    ");
        logger.LogTrace("╚██╗ ██╔╝╚════██║   ██║   ██║   ██║██║   ██║██║     ╚════██║    ");
        logger.LogTrace(" ╚████╔╝ ███████║   ██║   ╚██████╔╝╚██████╔╝███████╗███████║    ");
        logger.LogTrace("  ╚═══╝  ╚══════╝   ╚═╝    ╚═════╝  ╚═════╝ ╚══════╝╚══════╝    ");
        logger.LogTrace("");
    }
}