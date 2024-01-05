using RonSijm.VSTools.Lib.Features.ProjectFixing.Services;
using RonSijm.VSTools.Lib.Features.SolutionFixing.Services;

namespace RonSijm.VSTools.Lib.Features;

public class ReferenceFixerCore
{
    public void Fix(List<string> directoriesToInspect, List<string> projectReferences)
    {
        var projectReferenceFixer = new ProjectReferenceLocator();

        var mismatchingReferences = projectReferenceFixer.GetMismatchingReferences(directoriesToInspect, projectReferences);
        mismatchingReferences.ProjectsToFix.FixMismatchingReferences();

        var solutionFileReferenceFixer = new SolutionReferenceLocator();
        var results = solutionFileReferenceFixer.GetMismatchingReferences(directoriesToInspect, projectReferences);
        results.FixMismatchingReferences();
    }
}