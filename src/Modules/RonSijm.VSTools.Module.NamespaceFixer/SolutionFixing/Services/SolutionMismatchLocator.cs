using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.FileModels;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Core.FileOps;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;
using RonSijm.VSTools.Module.NamespaceFixer.Core;
using RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Services;
using RonSijm.VSTools.Module.NamespaceFixer.SolutionFixing.Models;

namespace RonSijm.VSTools.Module.NamespaceFixer.SolutionFixing.Services;

public class SolutionMismatchLocator(IAsyncLogger<SolutionMismatchLocator> logger, ProjectReferenceLoader projectReferenceLoader, MismatchDetector mismatchDetector) : IMismatchLocator
{
    public ushort Order => 3;

    public async Task<INamedCollection> GetMismatches(IFolderFixOptionsWithReferences options)
    {
        var result = new SolutionsToFixCollection
        {
            ObjectName = string.Join(',', options.DirectoriesToInspect)
        };

        var solutionFiles = GetSolutionFiles(options.DirectoriesToInspect);
        var projectFiles = projectReferenceLoader.GetProjectReferences(options);

        var loadedSolutions = solutionFiles.Select(async solution => await LoadSolution(solution, projectFiles));

        foreach (var loadedSolutionTask in loadedSolutions)
        {
            var loadResult = await loadedSolutionTask;
            result.Add(loadResult);
        }


        return result;
    }

    private async Task<SolutionToFixModel> LoadSolution(FileModel solution, ProjectFileContainer projectReferences)
    {
        var solutionFile = SolutionFile.Parse(solution.FileName);
        var projectItems = solutionFile.ProjectsInOrder;
        var result = new SolutionToFixModel(solution.FileName);

        // ReSharper disable once LoopCanBeConvertedToQuery - Justification: Creates unreadable code
        foreach (var project in projectItems)
        {
            if (project.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat)
            {
                var mismatchResult = mismatchDetector.FindMismatch(projectReferences, solution.FileName, project);

                if (mismatchResult is { IsT0: true })
                {
                    result.Add(mismatchResult.Value.AsT0);
                }
                else if (mismatchResult is { IsT1: true })
                {
                    result.Add(mismatchResult.Value.AsT1);
                }
            }
            else if (project.ProjectType == SolutionProjectType.SolutionFolder)
            {
                // Do Nothing
            }
            else
            {
                await logger.LogError("Could not find solution loader for Project Type {ProjectType}", project.ProjectType);
            }
        }

        return result;
    }

    private static List<FileModel> GetSolutionFiles(List<string> projectReferences)
    {
        var solutions = new List<FileModel>();
        FileLocator.FindFilesWithoutBinFolder("*.sln", projectReferences, solutions);
        return solutions;
    }
}