using RonSijm.VSTools.Sample.Lib5;

namespace RonSijm.VSTools.Sample.Lib4;

public static class OutWriter4
{
    public static void Write()
    {
        Console.WriteLine("Hello from Lib4");
        OutWriter5.Write();
    }
}