namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class ProjectMismatchLocator(ProjectReferenceLoader projectReferenceLoader, MismatchDetector mismatchDetector) : IMismatchLocator
{
    public ushort Order => 2;

    /// <inheritdoc />
    public OneOf<ItemsToFixResponse, CollectionToFixResponse> GetMismatches(CoreOptionsRequest options)
    {
        var result = new ItemsToFixResponse();

        var projectLoader = new ProjectFileLoader();
        var loadedProjects = projectLoader.OpenProjects(options.DirectoriesToInspect);

        var projectContainer = projectReferenceLoader.GetProjectReferences(options);

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