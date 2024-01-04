using RonSijm.VSTools.Lib.Features.ProjectFixing.Services;
using RonSijm.VSTools.Lib.Features.SolutionFixing.Services;

namespace RonSijm.VSTools.CLI;

internal class Program
{
    static void Main(string[] args)
    {
        var projectReferenceFixer = new ProjectReferenceLocator();

        // Directory with .sln or .csproj files to actually inspect
        var directoriesToInspect = new List<string> { "C:\\Dev\\Personal\\RonSijm.VSTools\\Sample" };

        // Directory with additional .csproj files to use as references
        var projectReferences = new List<string> { "C:\\Dev\\Personal\\RonSijm.VSTools\\src" };

        var mismatchingReferences = projectReferenceFixer.GetMismatchingReferences(directoriesToInspect, projectReferences);
        mismatchingReferences.ProjectsToFix.FixMismatchingReferences();

        var solutionFileReferenceFixer = new SolutionReferenceLocator();
        var results = solutionFileReferenceFixer.GetMismatchingReferences(directoriesToInspect, projectReferences);
        results.FixMismatchingReferences();
    }
}