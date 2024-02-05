namespace RonSijm.VSTools.CLI.Options.InputArgs;

public class OptionsFromArgsParser(ILogger<OptionsFromArgsParser> logger)
{
    public ParsedCLIOptionsModel Load(string[] args)
    {
        var cliParseResult = Parser.Default.ParseArguments<CLIOptionsModel>(args);

        if (cliParseResult.Errors.Any())
        {
            return null;
        }

        var baseOptions = CreateBaseOptionsFromInput(cliParseResult);

        cliParseResult.Value.Convert(baseOptions);

        // Forcing a config update when running in init mode, to save the cache.
        if (baseOptions.Mode == ModeEnum.CreateReferences && baseOptions.Run)
        {
            baseOptions.UpdateConfig = true;
        }

        return baseOptions;
    }

    private ParsedCLIOptionsModel CreateBaseOptionsFromInput(ParserResult<CLIOptionsModel> cliParseResult)
    {
        if (cliParseResult.Value.OptionsFile != null)
        {
            logger.LogDebug(" - Using Options File specified by CLI: {OptionsFile}", cliParseResult.Value.OptionsFile);
            return cliParseResult.Value.OptionsFile.LoadOptions();
        }

        var workingDirectory = Directory.GetCurrentDirectory();

        if (cliParseResult.Value.IgnoreSettingsFile == true)
        {
            logger.LogDebug(" - Using Working Directory: {workingDirectory}", workingDirectory);
            return new ParsedCLIOptionsModel { DirectoriesToInspect = [workingDirectory] };
        }

        var settingsFiles = Directory.GetFiles(workingDirectory, "*.VSToolSettings", System.IO.SearchOption.TopDirectoryOnly);

        if (settingsFiles.Length == 0)
        {
            logger.LogDebug(" - Using Directory: ({workingDirectory})", workingDirectory);
            return new ParsedCLIOptionsModel { DirectoriesToInspect = [workingDirectory] };
        }

        if (settingsFiles.Length == 1)
        {
            logger.LogDebug(" - Using settings file found in directory: ({settingsFiles})", settingsFiles[0]);
            return settingsFiles[0].LoadOptions();
        }

        logger.LogWarning("There are multiple settings file in this folder:");

        foreach (var settingsFile in settingsFiles)
        {
            logger.LogWarning($" - {settingsFile}");
        }

        logger.LogWarning($"You can either call the program with flag '--{nameof(CLIOptionsModel.IgnoreSettingsFile)}' to run in this directory,");
        logger.LogWarning("Or you should be able to double-click a settings file, and use a specific one.");

        return null;
    }
}