using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Services;

public static class ProjectReferenceFixer
{
    /// <summary>
    /// Actually fixes the Mismatching References.
    /// </summary>
    public static void FixMismatchingReferences(this IEnumerable<(ProjectItemElement reference, string expectedPath)> includesToFix)
    {
        foreach (var (reference, expectedPath) in includesToFix)
        {
            reference.Include = expectedPath;
            var project = reference.GetRoot();
            project.Save();
        }
    }
}