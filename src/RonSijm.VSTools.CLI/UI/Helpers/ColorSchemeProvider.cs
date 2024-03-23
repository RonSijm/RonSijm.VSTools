using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Helpers;

public static class ColorSchemeProvider
{
    public static ColorScheme ColorScheme { get; } = new()
    {
        Normal = new Terminal.Gui.Attribute(Color.White, Color.Black),
        Focus = new Terminal.Gui.Attribute(Color.BrightBlue, Color.Gray),
        HotFocus = new Terminal.Gui.Attribute(Color.BrightGreen, Color.DarkGray),
        HotNormal = new Terminal.Gui.Attribute(Color.Yellow, Color.BrightBlue)
    };

    public static ColorScheme DialogColorScheme { get; } = new()
    {
        Normal = new Terminal.Gui.Attribute(Color.BrightBlue, Color.Black),
    };
}