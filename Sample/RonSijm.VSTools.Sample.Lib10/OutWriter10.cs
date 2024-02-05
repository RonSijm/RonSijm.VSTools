namespace RonSijm.VSTools.Sample.Lib10;

public static class OutWriter10
{
    public static OutWriter10ResponseModelInFile Write()
    {
        return new OutWriter10ResponseModelInFile { Response = "Hello from Lib10" };
    }

    // ReSharper disable once RedundantNameQualifier
    public static RonSijm.VSTools.Sample.Lib10.OutWriter10ResponseModelInFile DoNothingWrite()
    {
        return new OutWriter10ResponseModelInFile { Response = "Hello from Lib10" };
    }
}