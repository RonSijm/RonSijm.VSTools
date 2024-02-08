namespace RonSijm.VSTools.Sample.Lib4.Objects;

public static class OutWriter4
{
    public static void Write()
    {
        Console.WriteLine("Hello from Lib4");

        // ReSharper disable RedundantNameQualifier - Intention full quantified namespace
        if (RonSijm.VSTools.Sample.Lib5.OutWriter5.ShouldWrite)
        {
            RonSijm.VSTools.Sample.Lib5.OutWriter5.Write();
        }
    }
}