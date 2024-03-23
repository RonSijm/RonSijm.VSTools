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
        ParsedCLIOptionsModel options;

        try
        {
            var json = File.ReadAllText(optionsFile);
            options = JsonSerializer.Deserialize<ParsedCLIOptionsModel>(json, JsonSettingsContainer.SettingsIndented);


            return options;
        }
        catch (Exception)
        {
            options = new ParsedCLIOptionsModel();
        }

        options.OptionsFile = optionsFile;

        if (string.IsNullOrWhiteSpace(options.WorkingDirectory))
        {
            var path = Path.GetDirectoryName(optionsFile);
            options.WorkingDirectory = path;
        }

        return options;
    }
}