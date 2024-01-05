using RonSijm.VSTools.Lib.Features.ProjectFixing.Abstractions;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Services;

public static class ProjectReferenceFixer
{
    /// <summary>
    /// Actually fixes the Mismatching References.
    /// </summary>
    public static void FixMismatchingReferences(this IEnumerable<(ItemReference reference, string expectedPath)> includesToFix)
    {
        foreach (var (reference, expectedPath) in includesToFix)
        {
            reference.SetPath(expectedPath);
            reference.SaveTarget.Save();
        }
    }
}