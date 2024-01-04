namespace RonSijm.VSTools.Sample.Lib1;

public static class OutWriter2
{
    public static void Write()
    {
        Console.WriteLine("Hello from Lib2");
        OutWriter3.Write();
    }
}