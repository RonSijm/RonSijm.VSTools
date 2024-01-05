using RonSijm.VSTools.Lib.Features;

namespace RonSijm.VSTools.CLI;

internal class Program
{
    static void Main()
    {
        // Directory with .sln or .csproj files to actually inspect
        var directoriesToInspect = new List<string> { "C:\\Dev\\Personal\\RonSijm.VSTools\\Sample" };

        // Directory with additional .csproj files to use as references
        var projectReferences = new List<string> { "C:\\Dev\\Personal\\RonSijm.VSTools\\src" };

        var referenceFixerCore = new ReferenceFixerCore();
        referenceFixerCore.Fix(directoriesToInspect, projectReferences);
    }
}