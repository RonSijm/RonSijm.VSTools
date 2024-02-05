namespace RonSijm.VSTools.Sample.Lib7;

public static class OutWriter7
{
    public static void Write()
    {
        Console.WriteLine("Hello from Lib7");

        // ReSharper disable RedundantNameQualifier - Justification: Used for test.
        if (Lib8.OutWriter8.ShouldWrite)
        {
            Lib8.OutWriter8.Write(true);
            Sample.Lib8.OutWriter8.Write(false);
            VSTools.Sample.Lib8.OutWriter8.Write(false);
            RonSijm.VSTools.Sample.Lib8.OutWriter8.Write(false);
        }
    }
}