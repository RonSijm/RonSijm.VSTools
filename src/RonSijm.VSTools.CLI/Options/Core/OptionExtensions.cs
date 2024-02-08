using RonSijm.VSTools.Lib.Features.Core.Options;

namespace RonSijm.VSTools.CLI.Options.Core;

public static class OptionExtensions
{
    public static void SaveOptions(this CLISaveOptionsModel coreOptionsRequest, string filePath)
    {
        var text = JsonSerializer.Serialize(coreOptionsRequest, JsonSettingsContainer.SettingsIndented);
        File.WriteAllText(filePath, text);
    }

    public static ParsedCLIOptionsModel LoadOptions(this string optionsFile)
    {
        var json = File.ReadAllText(optionsFile);
        var options = JsonSerializer.Deserialize<ParsedCLIOptionsModel>(json, JsonSettingsContainer.SettingsIndented);
        options.OptionsFile = optionsFile;

        return options;
    }
}