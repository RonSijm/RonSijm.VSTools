using RonSijm.VSTools.Lib.Features.Core;
using RonSijm.VSTools.Lib.Features.Core.Options.Models;
using RonSijm.VSTools.Lib.Features.CreateReferences.Interfaces;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class ProjectFileLoader(IEnumerable<IReferenceLoadingDecorator> referenceLoadingDecorator)
{
    public ProjectFileContainer GetProjectFiles(CoreOptionsRequest options)
    {
        var allProjectReferences = new ProjectFileContainer();
        FileLocator.FindFiles("*.csproj", options.ProjectReferences, allProjectReferences);
        FileLocator.FindFiles("*.csproj", options.DirectoriesToInspect, allProjectReferences);

        foreach (var loadingDecorator in referenceLoadingDecorator)
        {
            loadingDecorator.LoadReferences(options, allProjectReferences);
        }

        return allProjectReferences;
    }
}