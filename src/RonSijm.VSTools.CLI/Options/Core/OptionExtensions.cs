using RonSijm.VSTools.Lib.Features.Core.Options;

namespace RonSijm.VSTools.CLI.Options.Core;

public static class OptionExtensions
{
    public static void SaveOptions(this CoreOptionsRequest coreOptionsRequest, string filePath)
    {
        var text = JsonSerializer.Serialize(coreOptionsRequest, JsonSettingsContainer.SettingsIndented);
        File.WriteAllText(filePath, text);
    }

    public static void SaveOptions(this ParsedCLIOptionsModel cliOptions)
    {
        var saveOptions = cliOptions.Convert();

        var text = JsonSerializer.Serialize(saveOptions, JsonSettingsContainer.SettingsIndented);
        File.WriteAllText(cliOptions.OptionsFile, text);
    }

    public static ParsedCLIOptionsModel LoadOptions(this string optionsFile)
    {
        var json = File.ReadAllText(optionsFile);
        var options = JsonSerializer.Deserialize<ParsedCLIOptionsModel>(json, JsonSettingsContainer.SettingsIndented);
        options.OptionsFile = optionsFile;

        return options;
    }
}