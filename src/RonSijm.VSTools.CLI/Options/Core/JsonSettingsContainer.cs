using System.Text.Json.Serialization;

namespace RonSijm.VSTools.CLI.Options.Core;

public static class JsonSettingsContainer
{
    public static JsonSerializerOptions SettingsIndented { get; } = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new JsonStringEnumConverter()
        },
        WriteIndented = true
    };
}