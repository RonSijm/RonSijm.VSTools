using System.Threading.Tasks;

using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.CLI.Options.Services;

public static class OptionsParseServiceExtension
{
    public static async Task<ParsedCLIOptionsModel> CreateOptions(this CLICommandModel cliParseResult, IAsyncLogger logger = null)
    {
        if (cliParseResult.OptionsFile != null)
        {
            await logger.LogDebug(" - Using Options File specified by CLI: {OptionsFile}", cliParseResult.OptionsFile);

            var loadedOptions = cliParseResult.OptionsFile.LoadOptions();

            return PostfixOptionsDecorator(loadedOptions, cliParseResult);
        }

        var workingDirectory = Directory.GetCurrentDirectory();
        
        if (cliParseResult.IgnoreSettingsFile == true)
        {
            await logger.LogDebug(" - Using Working Directory: {workingDirectory}", workingDirectory);
            return PostfixOptionsDecorator(new ParsedCLIOptionsModel { DirectoriesToInspect = [workingDirectory], WorkingDirectory = workingDirectory }, cliParseResult);
        }

        var settingsFiles = Directory.GetFiles(workingDirectory, "*.VSToolSettings", System.IO.SearchOption.TopDirectoryOnly);

        if (settingsFiles.Length == 0)
        {
            await logger.LogDebug(" - Using Directory: ({workingDirectory})", workingDirectory);

            var converted = cliParseResult.Convert();
            converted.DirectoriesToInspect = [workingDirectory];
            converted.WorkingDirectory = workingDirectory;

            return PostfixOptionsDecorator(converted, cliParseResult);
        }

        if (settingsFiles.Length == 1)
        {
            await logger.LogDebug(" - Using settings file found in directory: ({settingsFiles})", settingsFiles[0]);
            return PostfixOptionsDecorator(settingsFiles[0].LoadOptions(), cliParseResult);
        }

        await logger.LogWarning("There are multiple settings file in this folder:");

        foreach (var settingsFile in settingsFiles)
        {
            await logger.LogWarning($" - {settingsFile}");
        }

        await logger.LogWarning($"You can either call the program with flag '--{nameof(CLICommandModel.IgnoreSettingsFile)}' to run in this directory,");
        await logger.LogWarning("Or you should be able to double-click a settings file, and use a specific one.");

        return null;
    }

    private static ParsedCLIOptionsModel PostfixOptionsDecorator(ParsedCLIOptionsModel options, CLICommandModel commandLineOptions)
    {
        if (options.Mode == ModeEnum.CreateReferences && options.RealRun == RunModeEnum.Real)
        {
            options.UpdateConfig = true;
        }

        var optionsWithFlags = commandLineOptions.Convert(options);

        return optionsWithFlags;
    }
}