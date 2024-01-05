using RonSijm.VSTools.Sample.Lib6;
using RonSijm.VSTools.Sample.Lib7;

namespace RonSijm.VSTools.Sample.Lib5;

public static class OutWriter5
{
    public static void Write()
    {
        Console.WriteLine("Hello from Lib5");
        OutWriter6.Write();
        OutWriter7.Write();
    }
}