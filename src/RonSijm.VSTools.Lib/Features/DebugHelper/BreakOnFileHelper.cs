namespace RonSijm.VSTools.Lib.Features.DebugHelper;

public static class BreakOnFileHelper
{
    [Conditional("DEBUG")]
    public static void BreakOnFile(string fileName)
    {
        // Add filename you want to break on
        if (fileName.Contains("RonSijm.VSTools.Sample.Lib14.csproj") || fileName.Contains("Lib14Renamed.csproj"))
        {

        }
    }

}