namespace RonSijm.VSTools.CLI.Options.Logging;

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
                logger.LogInformation(" - Press 'M' To do another to {mode} (Cold)", nameof(ModeEnum.FindMismatches));
                logger.LogInformation(" - Press 'C' To Change mode to {mode} (Cold)", nameof(ModeEnum.CreateReferences));
            }

            if (currentOptions.Mode == ModeEnum.CreateReferences)
            {
                logger.LogInformation(" - Press 'C' To do another to {mode} (Cold)", nameof(ModeEnum.CreateReferences));
                logger.LogInformation(" - Press 'M' To Change mode to {mode} (Cold)", nameof(ModeEnum.FindMismatches));
            }

            logger.LogInformation(" - Press 'R' To Run {mode} (For Real)", currentOptions.Mode);

            if(currentOptions.Mode == ModeEnum.FindMismatches && (currentOptions.ProjectReferencesById == null || !currentOptions.ProjectReferencesById.Any()))
            {
                logger.DisplayNoReferencesWarning();
            }

            logger.LogInformation(" - Press 'Q' to {Quit}", "Quit");

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

            if (key.KeyChar is 'c' or 'C')
            {
                return x =>
                {
                    x.Run = false;
                    x.Mode = ModeEnum.CreateReferences;
                };
            }

            if (key.KeyChar is 'm' or 'M')
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