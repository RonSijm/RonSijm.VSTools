using RonSijm.VSTools.Lib.Features.Core;
using RonSijm.VSTools.Lib.Features.Core.Options.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.Core;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Abstractions;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Extensions;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class ProjectMismatchLocator(ProjectFileLoader projectFileLoader, MismatchDetector mismatchDetector) : IMismatchLocator
{
    /// <inheritdoc />
    public OneOf<ItemsToFixResponse, CollectionToFixResponse> GetMismatches(CoreOptionsRequest options)
    {
        var result = new ProjectsToFixResponse();

        var projectLoader = new ProjectLoader();
        var loadedProjects = projectLoader.LoadProjects(options.DirectoriesToInspect);

        var projectContainer = projectFileLoader.GetProjectFiles(options);

        foreach (var projectRootElement in loadedProjects)
        {
            var references = projectRootElement.GetReferencedProjects().ToList();
            FindMismatches(projectContainer, references, result, projectRootElement.FullPath);
        }

        FindMismatchInIncludeReferences(options, loadedProjects, result);

        return result;
    }

    private void FindMismatchInIncludeReferences(CoreOptionsRequest options, List<ProjectRootElement> loadedProjects, ItemsToFixResponse result)
    {
        var allIncludeReferences = new ProjectFileContainer();
        FileLocator.FindFiles("*.props", options.ProjectReferences, allIncludeReferences);
        FileLocator.FindFiles("*.props", options.DirectoriesToInspect, allIncludeReferences);

        foreach (var projectRootElement in loadedProjects)
        {
            var includes = projectRootElement.GetIncludes().ToList();
            FindMismatches(allIncludeReferences, includes, result, projectRootElement.FullPath);
        }
    }

    private void FindMismatches(ProjectFileContainer projectReferences, List<ItemReference> references, ItemsToFixResponse result, string projectPath)
    {
        foreach (var mismatchResult in references.Select(reference => mismatchDetector.FindMismatch(projectReferences, projectPath, reference)))
        {
            if (mismatchResult is { IsT0: true })
            {
                result.Add(mismatchResult.Value.AsT0);
            }
            else if (mismatchResult is { IsT1: true })
            {
                result.Add(mismatchResult.Value.AsT1);
            }
        }
    }
}