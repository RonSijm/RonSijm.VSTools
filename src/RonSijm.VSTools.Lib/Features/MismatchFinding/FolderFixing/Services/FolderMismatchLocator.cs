using RonSijm.VSTools.Lib.Features.MismatchFinding.FolderFixing.Models;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.FolderFixing.Services;

public class FolderMismatchLocator : IMismatchLocator
{
    public ushort Order => 1;

    public OneOf<ItemsToFixResponse, CollectionToFixResponse> GetMismatches(CoreOptionsRequest options)
    {
        var result = new ItemsToFixResponse();

        if (options.FolderFixMode == FolderFixModeEnum.DoNothing)
        {
            return result;
        }

        var projectLoader = new ProjectFileLoader();
        var loadedProjects = projectLoader.OpenProjects(options.DirectoriesToInspect);

        foreach (var projectRootElement in loadedProjects)
        {
            var directoryName = Path.GetDirectoryName(projectRootElement.FullPath);

            if (directoryName == null)
            {
                continue;
            }

            var directoryParts = directoryName.Split(Path.DirectorySeparatorChar);
            var lastFolderName = directoryParts[^1];

            var fullFileName = Path.GetFileName(projectRootElement.FullPath);

            if (fullFileName == null)
            {
                continue;
            }

            var fileName = fullFileName.Replace(".csproj", string.Empty, StringComparison.OrdinalIgnoreCase);

            if (lastFolderName != fileName)
            {
                // If there is a casing issue, first rename the item to "bak-" - because windows doesn't let you rename case-sensitive to the same filename.
                var casingIssue = lastFolderName.Equals(fileName, StringComparison.InvariantCultureIgnoreCase);

                if (options.FolderFixMode == FolderFixModeEnum.RenameProject)
                {
                    var expectedProjectName = $"{lastFolderName}.csproj";
                    var expectedProjectPath = Path.Combine(directoryName, casingIssue ? $"bak-{expectedProjectName}" : expectedProjectName);

                    result.Add(new ProjectToRenameFixer { CurrentItemValue = projectRootElement.FullPath, ExpectedItemValue = expectedProjectPath, ExpectedItemDisplayValue = expectedProjectName, CurrentItemDisplayValue = fullFileName });
                }
                else
                {
                    var expectedItemValue = casingIssue ? $"bak-{fileName}" : fileName;

                    result.Add(new FolderToRenameResult { CurrentItemValue = lastFolderName, ExpectedItemValue = expectedItemValue, ExpectedItemDisplayValue = expectedItemValue, CurrentItemDisplayValue = lastFolderName });
                }
            }
        }

        var duplicateProjects = result.ItemsToFix.Where(x => x is ProjectToRenameFixer).GroupBy(x => x.ExpectedItemDisplayValue).ToList();

        foreach (var duplicateProject in duplicateProjects.Where(x => x.Count() > 1))
        {
            result.Add(new ErrorWhileMatchingResult { Error = $"Cannot rename project to {duplicateProject.Key} - Because there are {duplicateProject.Count()} projects resulting in the same name.", ErrorReferences = duplicateProject.Select(x => x.CurrentItemValue).ToList() });

            foreach (var fixableItem in duplicateProject)
            {
                result.Remove(fixableItem);
            }
        }

        return result;
    }
}