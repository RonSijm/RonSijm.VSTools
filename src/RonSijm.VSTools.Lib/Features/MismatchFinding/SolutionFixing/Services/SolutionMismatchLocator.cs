using RonSijm.VSTools.Lib.Features.Core;
using RonSijm.VSTools.Lib.Features.Core.Options.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.Core;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;
using RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Services;

public class SolutionMismatchLocator(ILogger<SolutionMismatchLocator> logger, ProjectFileLoader projectFileLoader, MismatchDetector mismatchDetector) : IMismatchLocator
{
    public OneOf<ItemsToFixResponse, CollectionToFixResponse> GetMismatches(CoreOptionsRequest options)
    {
        var result = new CollectionToFixResponse();

        var solutionFiles = GetSolutionFiles(options.DirectoriesToInspect);
        var projectFiles = projectFileLoader.GetProjectFiles(options);

        result.AddRange(solutionFiles.Select(solution => LoadSolution(solution, projectFiles)).Where(solutionToFixModel => solutionToFixModel.HasItems));

        return result;
    }

    private SolutionToFixModel LoadSolution(ProjectFileModel solution, ProjectFileContainer projectReferences)
    {
        var solutionFile = SolutionFile.Parse(solution.File);
        var projectItems = solutionFile.ProjectsInOrder;
        var result = new SolutionToFixModel(solution.File);

        // ReSharper disable once LoopCanBeConvertedToQuery - Justification: Creates unreadable code
        foreach (var project in projectItems)
        {
            if (project.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat)
            {
                var mismatchResult = mismatchDetector.FindMismatch(projectReferences, solution.File, project);

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

    private static List<ProjectFileModel> GetSolutionFiles(List<string> projectReferences)
    {
        var solutions = new List<ProjectFileModel>();
        FileLocator.FindFiles("*.sln", projectReferences, solutions);
        return solutions;
    }
}