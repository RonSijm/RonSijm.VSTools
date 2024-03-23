using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;
using ItemReference = RonSijm.VSTools.Core.DataContracts.FileModels.ItemReference;

namespace RonSijm.VSTools.Module.NamespaceFixer.Core;

public class MismatchDetector
{
    public OneOf<ErrorWhileMatchingResult, ProjectInProjectToFixResult>? FindMismatch(ProjectFileContainer projectReferences, string projectPath, ItemReference reference)
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

        if (reference.Path != expectedPath)
        {
            var itemToFix = new ProjectInProjectToFixResult
            {
                ExpectedItemValue = expectedPath,
                Reference = reference,
                ExpectedItemDisplayValue = expectedPath,
                CurrentItemValue = reference.Path,
                CurrentItemDisplayValue = reference.Path,
            };

            return itemToFix;
        }

        return null;
    }
}