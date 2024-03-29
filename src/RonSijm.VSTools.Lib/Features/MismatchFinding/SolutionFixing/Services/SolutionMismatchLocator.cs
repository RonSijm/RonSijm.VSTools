﻿namespace RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Services;

public class SolutionMismatchLocator(ILogger<SolutionMismatchLocator> logger, ProjectReferenceLoader projectReferenceLoader, MismatchDetector mismatchDetector) : IMismatchLocator
{
    public ushort Order => 3;

    public INamedCollection GetMismatches(CoreOptionsRequest options)
    {
        var result = new SolutionsToFixCollection
        {
            ObjectName = string.Join(',', options.DirectoriesToInspect)
        };

        var solutionFiles = GetSolutionFiles(options.DirectoriesToInspect);
        var projectFiles = projectReferenceLoader.GetProjectReferences(options);

        var loadedSolutions = solutionFiles.Select(solution => LoadSolution(solution, projectFiles)).Where(solutionToFixModel => solutionToFixModel.HasRelevantItems);
        
        result.AddRange(loadedSolutions);

        return result;
    }

    private SolutionToFixModel LoadSolution(FileModel solution, ProjectFileContainer projectReferences)
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
                logger.LogError("Could not find solution loader for Project Type {ProjectType}", project.ProjectType);
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