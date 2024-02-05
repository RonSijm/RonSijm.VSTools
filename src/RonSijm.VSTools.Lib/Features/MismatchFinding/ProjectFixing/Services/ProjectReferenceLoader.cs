namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class ProjectReferenceLoader(IEnumerable<IReferenceLoadingDecorator> referenceLoadingDecorator)
{
    public ProjectFileContainer GetProjectReferences(CoreOptionsRequest options)
    {
        var allProjectReferences = new ProjectFileContainer();
        FileLocator.FindFilesWithoutBinFolder("*.csproj", options.ProjectReferences, allProjectReferences);
        FileLocator.FindFilesWithoutBinFolder("*.csproj", options.DirectoriesToInspect, allProjectReferences);

        foreach (var loadingDecorator in referenceLoadingDecorator)
        {
            loadingDecorator.LoadReferences(options, allProjectReferences);
        }

        return allProjectReferences;
    }
}