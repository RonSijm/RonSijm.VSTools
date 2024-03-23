using System.Threading.Tasks;

using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.CLI.Options.Logging;

public class InteractiveOptionsHelper(IAsyncLogger<InteractiveOptionsHelper> logger)
{
    public async Task<Action<ParsedCLIOptionsModel>> PrintInputOptions(VSToolResult result, ParsedCLIOptionsModel currentOptions)
    {
        if (currentOptions.Silent)
        {
            return null;
        }

        if (currentOptions.RealRun == RunModeEnum.Real)
        {
            var haveInnerReturnables = (IHaveInnerReturnables)result;

            await logger.LogInformation("Runing finished.");

            if (haveInnerReturnables.RelevantItemCount != 0)
            {
                await logger.LogInformation("Above are the results that were fixed.");
                await logger.LogInformation("You could do another cold-run to ensure everything is fixed.");
            }
            else
            {
                await logger.LogInformation("Seems like this was nothing to fix...");
            }
        }
        else
        {
            var haveInnerReturnables = (IHaveInnerReturnables)result;

            await logger.LogInformation("Cold-run finished.");
            await logger.LogInformation("Note that in the real run more issues might arise.");
            await logger.LogInformation("For example, if you rename a project, it's namespace will change,");
            await logger.LogInformation("So we'll also have to update the namespace references.");

            if (haveInnerReturnables.RelevantItemCount != 0)
            {
                await logger.LogInformation("Above are the results that were found.");
                await logger.LogInformation("Nothing has been changed yet. Do a real run to persist these changes.");
            }
            else
            {
                await logger.LogInformation("Seems like this is nothing to fix...");
            }
        }

        if (result.HasErrors)
        {
            await logger.LogWarning("You seem to have errors - you're advised to fix them first.");
            await logger.LogWarning("For example, by adding missing project references to the ProjectReferences settings,");
            await logger.LogWarning("Or by removing projects with duplicate project names.");
        }

        await logger.LogInformation("What would you like to do next?");

        do
        {
            if (currentOptions.Mode == ModeEnum.FindMismatches)
            {
                await logger.LogInformation(" - Press 'M' To do another to {mode} (Cold)", nameof(ModeEnum.FindMismatches));
                await logger.LogInformation(" - Press 'C' To Change mode to {mode} (Cold)", nameof(ModeEnum.CreateReferences));
            }

            if (currentOptions.Mode == ModeEnum.CreateReferences)
            {
                await logger.LogInformation(" - Press 'C' To do another to {mode} (Cold)", nameof(ModeEnum.CreateReferences));
                await logger.LogInformation(" - Press 'M' To Change mode to {mode} (Cold)", nameof(ModeEnum.FindMismatches));
            }

            await logger.LogInformation(" - Press 'R' To Run {mode} (For Real)", currentOptions.Mode);

            if(currentOptions.Mode == ModeEnum.FindMismatches && (currentOptions.ProjectReferencesById == null || !currentOptions.ProjectReferencesById.Any()))
            {
                await logger.DisplayNoReferencesWarning();
            }

            await logger.LogInformation(" - Press 'Q' to {Quit}", "Quit");

            var key = Console.ReadKey();

            if (key.KeyChar is 'r' or 'R')
            {
                return x =>
                {
                    x.UpdateConfig = currentOptions.Mode == ModeEnum.CreateReferences || currentOptions.UpdateConfig;
                    x.RealRun = RunModeEnum.Real;
                    x.Mode = currentOptions.Mode;
                };
            }

            if (key.KeyChar is 'c' or 'C')
            {
                return x =>
                {
                    x.RealRun = RunModeEnum.Preview;
                    x.Mode = ModeEnum.CreateReferences;
                };
            }

            if (key.KeyChar is 'm' or 'M')
            {
                return x =>
                {
                    x.RealRun = RunModeEnum.Preview;
                    x.Mode = ModeEnum.FindMismatches;
                };
            }

            if (key.KeyChar is 'q' or 'q')
            {
                return null;
            }

            await logger.LogError("'{inputKey}' is invalid input.", key.KeyChar);
            await logger.LogError("These are the available options:");
        } while (true);
    }
}