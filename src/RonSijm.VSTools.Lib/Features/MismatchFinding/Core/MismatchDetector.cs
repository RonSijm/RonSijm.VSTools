using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Abstractions;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.Core;

public class MismatchDetector
{
    public OneOf<ErrorWhileMatchingResult, ItemToFixResult>? FindMismatch(ProjectFileContainer projectReferences, string projectPath, ItemReference reference)
    {
        var fileName = Path.GetFileName(reference.Path);

        if (fileName == null)
        {
            return new ErrorWhileMatchingResult { Reference = reference, Error = "Filename could not be determined" };
        }

        var referencePaths = projectReferences.FindProject(fileName);

        if (referencePaths.Count == 0)
        {
            return new ErrorWhileMatchingResult { Reference = reference, Error = "Reference not found", ErrorReferences = [fileName] };
        }

        if (referencePaths.Count > 1)
        {
            return new ErrorWhileMatchingResult { Reference = reference, Error = "Found more than one reference", ErrorReferences = referencePaths.ToList() };
        }

        var actualPath = referencePaths.First();

        var fullExpectedPath = Path.GetRelativePath(projectPath, actualPath);
        var expectedPath = fullExpectedPath[3..];

        var combinedPath = Path.Combine(projectPath, $"..\\{reference.Path}");
        var currentPath = Path.GetFullPath(combinedPath);

        if (reference.Path != expectedPath)
        {
            var itemToFix = new ItemToFixResult
            {
                ExpectedPath = expectedPath,
                Reference = reference,
                AbsoluteExpectedItemPath = actualPath,
                AbsoluteCurrentItemPath = currentPath,
                AbsoluteProjectPath = projectPath
            };

            return itemToFix;
        }

        return null;
    }
}