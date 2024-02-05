// ReSharper disable once RedundantUsingDirective - Intentionally, to also fix unused namespaces.
using RonSijm.VSTools.Sample.Lib9.WriterInSubfolder;

namespace RonSijm.VSTools.Sample.Lib8;

public static class OutWriter8
{
    public static bool ShouldWrite => true;

    public static void Write(bool shouldWrite)
    {
        if (shouldWrite)
        {
            Console.WriteLine("Hello from Lib8");
        }
    }
}