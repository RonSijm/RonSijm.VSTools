// ReSharper disable RedundantUsingDirective

using RonSijm.VSTools.Sample.Lib1;
using RonSijm.VSTools.Sample.Lib10;
using RonSijm.VSTools.Sample.Lib11;
using RonSijm.VSTools.Sample.Lib12;
using RonSijm.VSTools.Sample.Lib13;
using RonSijm.VSTools.Sample.Lib13.Project;
using RonSijm.VSTools.Sample.Lib14;
using RonSijm.VSTools.Sample.Lib2.BinWriterInSubfolder;
using RonSijm.VSTools.Sample.Lib4.Objects;

namespace RonSijm.VSTools.Sample.ConsoleApp;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World from Program!");
        OutWriter1.Write();
        OutWriter2.Write();
        OutWriter4.Write();
        OutWriter9.Write();

        // ReSharper disable SuggestVarOrType_SimpleTypes
        OutWriter10ResponseModelInFile unusedResult1 = OutWriter10.Write();
        Console.WriteLine(unusedResult1.Response);

        // ReSharper disable RedundantNameQualifier
        Lib10.OutWriter10ResponseModelInFile unusedResult2 = OutWriter10.Write();
        Sample.Lib10.OutWriter10ResponseModelInFile unusedResult3 = OutWriter10.Write();
        VSTools.Sample.Lib10.OutWriter10ResponseModelInFile unusedResult4 = OutWriter10.Write();
        RonSijm.VSTools.Sample.Lib10.OutWriter10ResponseModelInFile unusedResult5 = OutWriter10.Write();
        
        Console.WriteLine(Lib11.OutWriter11.Write().Response);

        Console.WriteLine(Sample.Lib12.OutWriter12.Write().Response);

        OutWriter13.Write();

        OutWriter14.Write();
    }
}