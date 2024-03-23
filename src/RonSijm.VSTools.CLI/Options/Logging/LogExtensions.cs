using System.Threading.Tasks;

using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.CLI.Options.Logging;

public static class LogExtensions
{
    public static async Task DisplayNoReferencesWarning<T>(this IAsyncLogger<T> logger)
    {
        await logger.LogWarning("Warning! - It looks like there are no project references by {mode} mode.", nameof(ModeEnum.CreateReferences));
        await logger.LogWarning("You're advised to do an {mode} first, if you plan to rename any of the project files...", nameof(ModeEnum.CreateReferences));
    }
}