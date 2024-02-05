using System.Runtime.CompilerServices;

namespace RonSijm.VSTools.Lib.Features.DebugHelper;

public static class BreakOnFileHelper
{
    [Conditional("DEBUG")]
    public static void BreakOnFile(string fileName, [CallerMemberName] string memberName = "")
    {
        // Add filename you want to break on
        //if (fileName.Contains("OutWriter13.cs"))
        if (fileName.Contains("_Imports.razor"))
        {

        }
    }
}