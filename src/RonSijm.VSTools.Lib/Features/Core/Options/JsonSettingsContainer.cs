namespace RonSijm.VSTools.Lib.Features.Core.Options;

public static class JsonSettingsContainer
{
    public static JsonSerializerOptions SettingsIndented { get; } = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new JsonStringEnumConverter()
        },
        WriteIndented = true
    };
}