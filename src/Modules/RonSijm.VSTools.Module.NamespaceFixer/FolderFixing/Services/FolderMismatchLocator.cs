using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Core.FileOps;
using RonSijm.VSTools.Module.NamespaceFixer.FolderFixing.Models;
using RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Services;

namespace RonSijm.VSTools.Module.NamespaceFixer.FolderFixing.Services;

public class FolderMismatchLocator : IMismatchLocator
{
    public ushort Order => 1;

    public Task<INamedCollection> GetMismatches(IFolderFixOptionsWithReferences options)
    {
        var result = new FoldersToFixCollection
        {
            ObjectName = string.Join(',', options.DirectoriesToInspect)
        };

        if (options.FolderFixMode == FolderFixModeEnum.DoNothing)
        {
            return Task.FromResult<INamedCollection>(result);
        }

        var projectLoader = new ProjectFileLoader();
        var loadedProjects = projectLoader.OpenProjects(options.DirectoriesToInspect);

        foreach (var loadedModel in loadedProjects)
        {
            var directoryName = Path.GetDirectoryName(loadedModel.File.FileName);

            if (directoryName == null)
            {
                continue;
            }

            var directoryParts = directoryName.Split(Path.DirectorySeparatorChar);
            var lastFolderName = directoryParts[^1];

            var fullFileName = Path.GetFileName(loadedModel.File.FileName);

            if (fullFileName == null)
            {
                continue;
            }

            var fileName = fullFileName.Replace(".csproj", string.Empty, StringComparison.OrdinalIgnoreCase);

            if (lastFolderName != fileName)
            {
                if (options.FolderFixMode == FolderFixModeEnum.RenameProject)
                {
                    var expectedProjectName = $"{lastFolderName}.csproj";
                    var expectedProjectPath = Path.Combine(directoryName, expectedProjectName);

                    result.Add(new ProjectToRenameFixer
                    {
                        CurrentItemValue = loadedModel.File.FileName,
                        ExpectedItemValue = expectedProjectPath,
                        ExpectedItemDisplayValue = expectedProjectName,
                        CurrentItemDisplayValue = fullFileName,
                    });
                }
                else
                {
                    result.Add(new FolderToRenameResult { CurrentItemValue = lastFolderName, ExpectedItemValue = fileName, ExpectedItemDisplayValue = fileName, CurrentItemDisplayValue = lastFolderName });
                }
            }
        }

        var duplicateProjects = result.InnerItems.Where(x => x is ProjectToRenameFixer).Cast<ProjectToRenameFixer>().GroupBy(x => x.ExpectedItemDisplayValue).ToList();

        foreach (var duplicateProject in duplicateProjects.Where(x => x.Count() > 1))
        {
            result.Add(new ErrorWhileMatchingResult { Error = $"Cannot rename project to {duplicateProject.Key} - Because there are {duplicateProject.Count()} projects resulting in the same name.", ErrorReferences = duplicateProject.Select(x => x.CurrentItemValue).ToList() });

            foreach (var fixableItem in duplicateProject)
            {
                result.Remove(fixableItem);
            }
        }

        return Task.FromResult<INamedCollection>(result);
    }
}