using System;
using RonSijm.VSTools.Sample.Lib3;

namespace RonSijm.VSTools.Sample.Lib2.WriterInSubfolder;

public static class OutWriter2
{
    public static void Write()
    {
        Console.WriteLine("Hello from Lib2");
        OutWriter3.Write();
    }
}