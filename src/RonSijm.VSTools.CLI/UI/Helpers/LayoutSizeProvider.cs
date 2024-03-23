using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI.Helpers;

public static class LayoutSizeProvider
{
    public static int HorizontalLayoutWith => 130;
    public static int HeaderSize => HorizontalLayout ? 6 : 13;
    public static int HeaderWidth => HorizontalLayout ? 125 : 62;
    public static bool HorizontalLayout => Console.WindowWidth > HorizontalLayoutWith;

    public static Dim HeaderSizeDim => Dim.Function(() => HeaderSize);
    public static Dim HeaderWidthDim => Dim.Function(() => HeaderWidth);

    public static int LayoutMargin => 0;
}