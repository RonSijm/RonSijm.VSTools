namespace RonSijm.VSTools.Lib.Features.DebugHelper;

public static class BreakOnFileHelper
{
    [Conditional("DEBUG")]
    public static void BreakOnFile(string fileName, [CallerMemberName] string memberName = "")
    {
        // Add filename you want to break on
        if (fileName.Contains("Home.razor")
            //|| fileName.Contains("OutWriter3.cs")
            )
        {

        }
    }

    [Conditional("DEBUG")]
    public static void BreakOnNamespaceLookup(string nameSpace, [CallerMemberName] string memberName = "")
    {
        if (nameSpace.Contains("WriteRecordInSubfolder")
            || nameSpace.Contains("Whatever")
           )
        {

        }
    }
}