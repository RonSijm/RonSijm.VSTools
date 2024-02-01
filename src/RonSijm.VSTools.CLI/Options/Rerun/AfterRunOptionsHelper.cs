using RonSijm.VSTools.Lib.Features.Core;
using RonSijm.VSTools.Lib.Features.Core.Options.Models;

namespace RonSijm.VSTools.CLI.Options.Rerun;

public class AfterRunOptionsHelper(ILogger<AfterRunOptionsHelper> logger)
{
    public Action<ParsedCLIOptionsModel> PrintInputOptions(VSToolResult result, ParsedCLIOptionsModel currentOptions)
    {
        if (currentOptions.Silent)
        {
            return null;
        }

        if (currentOptions.Run)
        {
            logger.LogInformation("Runing finished.");

            if (result.HasItems)
            {
                logger.LogInformation("Above are the results that were fixed.");
                logger.LogInformation("You could do another cold-run to ensure everything is fixed.");
            }
            else
            {
                logger.LogInformation("Seems like this was nothing to fix...");
            }
        }
        else
        {
            logger.LogInformation("Cold-run finished.");

            if (result.HasItems)
            {
                logger.LogInformation("Above are the results that were found.");
                logger.LogInformation("Nothing has been changed yet. Do a real run to persist these changes.");
            }
            else
            {
                logger.LogInformation("Seems like this is nothing to fix...");
            }
        }

        if (result.HasErrors)
        {
            logger.LogWarning("You seem to have errors - you're advised to fix them first.");
            logger.LogWarning("For example, by adding missing project references to the ProjectReferences settings,");
            logger.LogWarning("Or by removing projects with duplicate project names.");
        }

        logger.LogInformation("What would you like to do next?");

        do
        {
            if (currentOptions.Mode == ModeEnum.FindMismatches)
            {
                logger.LogInformation(" - Press 'S' To do another to Scan (Cold)");
                logger.LogInformation(" - Press 'I' To Change mode to Init (Cold)");
            }

            if (currentOptions.Mode == ModeEnum.CreateReferences)
            {
                logger.LogInformation(" - Press 'I' To do another to Init (Cold)");
                logger.LogInformation(" - Press 'S' To Change mode to Scan (Cold)");
            }

            logger.LogInformation(" - Press 'R' To Run (For Real)");

            if(currentOptions.Mode == ModeEnum.FindMismatches && (currentOptions.ProjectReferencesById == null || !currentOptions.ProjectReferencesById.Any()))
            {
                logger.LogWarning("Warning! - It looks like there are no project references by Init mode.");
                logger.LogWarning("You're advised to do an Init first, if you plan to rename any of the project files...");
            }

            logger.LogInformation(" - Press 'Q' to Quit");

            var key = Console.ReadKey();

            if (key.KeyChar is 'r' or 'R')
            {
                return x =>
                {
                    x.UpdateConfig = currentOptions.Mode == ModeEnum.CreateReferences || currentOptions.UpdateConfig;
                    x.Run = true;
                    x.Mode = currentOptions.Mode;
                };
            }

            if (key.KeyChar is 'i' or 'I')
            {
                return x =>
                {
                    x.Run = false;
                    x.Mode = ModeEnum.CreateReferences;
                };
            }

            if (key.KeyChar is 's' or 'S')
            {
                return x =>
                {
                    x.Run = false;
                    x.Mode = ModeEnum.FindMismatches;
                };
            }

            if (key.KeyChar is 'q' or 'q')
            {
                return null;
            }

            logger.LogError("'{inputKey}' is invalid input.", key.KeyChar);
            logger.LogError("These are the available options:");
        } while (true);
    }
}