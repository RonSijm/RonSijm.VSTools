using System.Threading.Tasks;

using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.CLI.Options.Services;

public class OptionsLoggingService(IAsyncLogger<OptionsLoggingService> logger)
{
    public async Task LogActions(ParsedCLIOptionsModel options)
    {
        if (options.Mode == ModeEnum.FindMismatches)
        {
            await logger.LogInformation(options.RealRun == RunModeEnum.Real ? "Fixing reference..." : "Doing preview run...");

            await logger.LogInformation("Fixing Directories:");

            foreach (var directoryToInspect in options.DirectoriesToInspect)
            {
                await logger.LogInformation($" - {directoryToInspect}");
            }

            if (options.ProjectReferences != null)
            {
                await logger.LogInformation("Using extra directories:");

                foreach (var projectReference in options.ProjectReferences)
                {
                    await logger.LogInformation($" - {projectReference}");
                }
            }

            if (options.ProjectReferencesById != null && options.ProjectReferencesById.Any())
            {
                await logger.LogInformation("Using [{projectReferenceCount}] Project References from config.", options.ProjectReferencesById.Count);
            }
            else
            {
                await logger.DisplayNoReferencesWarning();
            }
        }
        else if (options.Mode == ModeEnum.CreateReferences)
        {
            await logger.LogInformation("Creating Reference Ids for projects..");
        }
    }
}