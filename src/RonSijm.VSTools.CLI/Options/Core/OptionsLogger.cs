using RonSijm.VSTools.Lib.Features.Core.Options.Models;

namespace RonSijm.VSTools.CLI.Options.Core;

public class OptionsLogger(ILogger<OptionsLogger> logger)
{
    public void LogActions(ParsedCLIOptionsModel options)
    {
        if (options.Mode == ModeEnum.FindMismatches)
        {
            logger.LogInformation(options.Run ? "Fixing reference..." : "Doing cold run...");

            logger.LogInformation("Fixing Directories:");

            foreach (var directoryToInspect in options.DirectoriesToInspect)
            {
                logger.LogInformation($" - {directoryToInspect}");
            }

            if (options.ProjectReferences != null)
            {
                logger.LogInformation("Using extra directories:");

                foreach (var projectReference in options.ProjectReferences)
                {
                    logger.LogInformation($" - {projectReference}");
                }
            }

            if (options.ProjectReferencesById != null && options.ProjectReferencesById.Any())
            {
                logger.LogInformation("Using [{projectReferenceCount}] Project References from config.", options.ProjectReferencesById.Count);
            }
            else
            {
                logger.LogWarning("Warning! - It looks like there are no project references by Init mode.");
                logger.LogWarning("You're advised to do an Init first, if you plan to rename any of the project files...");
            }
        }
        else if (options.Mode == ModeEnum.CreateReferences)
        {
            logger.LogInformation("Creating Reference Ids for projects..");
        }
    }
}