namespace RonSijm.VSTools.CLI.Options.Logging;

public static class LogExtensions
{
    public static void DisplayNoReferencesWarning<T>(this ILogger<T> logger)
    {
        logger.LogWarning("Warning! - It looks like there are no project references by {mode} mode.", nameof(ModeEnum.CreateReferences));
        logger.LogWarning("You're advised to do an {mode} first, if you plan to rename any of the project files...", nameof(ModeEnum.CreateReferences));
    }
}