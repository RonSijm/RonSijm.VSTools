using RonSijm.VSTools.Sample.Lib1;
using RonSijm.VSTools.Sample.Lib2;
using RonSijm.VSTools.Sample.Lib4;

namespace RonSijm.VSTools.Sample.ConsoleApp;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World from Program!");
        OutWriter1.Write();
        OutWriter2.Write();
        OutWriter4.Write();
    }
}