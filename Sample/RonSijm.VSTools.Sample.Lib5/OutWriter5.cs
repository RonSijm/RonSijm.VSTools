namespace RonSijm.VSTools.Sample.Lib1;

public static class OutWriter5
{
    public static void Write()
    {
        Console.WriteLine("Hello from Lib5");
        OutWriter6.Write();
        OutWriter7.Write();
    }
}