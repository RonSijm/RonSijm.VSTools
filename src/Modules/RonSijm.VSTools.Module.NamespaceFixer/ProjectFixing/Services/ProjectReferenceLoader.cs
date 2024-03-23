using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;
using RonSijm.VSTools.Core.DataContracts.References;
using RonSijm.VSTools.Core.FileOps;

namespace RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Services;

public class ProjectReferenceLoader(IEnumerable<IReferenceLoadingDecorator> referenceLoadingDecorator)
{
    public ProjectFileContainer GetProjectReferences(IFolderFixOptionsWithReferences options)
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