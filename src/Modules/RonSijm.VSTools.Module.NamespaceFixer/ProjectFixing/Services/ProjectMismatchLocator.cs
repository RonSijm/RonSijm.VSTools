using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Core.FileOps;
using RonSijm.VSTools.Module.NamespaceFixer.Core;
using RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Extensions;
using RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Helpers;

namespace RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Services;

public class ProjectMismatchLocator(ProjectReferenceLoader projectReferenceLoader, MismatchDetector mismatchDetector) : IMismatchLocator
{
    public ushort Order => 2;

    /// <inheritdoc />
    public Task<INamedCollection> GetMismatches(IFolderFixOptionsWithReferences options)
    {
        var target = new ProjectsReferencesToFixCollection();

        var projectLoader = new ProjectFileLoader();

        target.ObjectName = string.Join(',', options.DirectoriesToInspect);
        var projectReferences = projectReferenceLoader.GetProjectReferences(options);

        var loadedProjects = projectLoader.OpenProjects(options.DirectoriesToInspect);
        var loadedProjectWithModel = loadedProjects.Select(x => new Tuple<ProjectLoadedModel, ProjectsReferencesToFixModel>(x, new ProjectsReferencesToFixModel { ObjectName = x.File.FileName })).ToList();

        foreach (var projectLoadedModel in loadedProjectWithModel)
        {
            target.Add(projectLoadedModel.Item2);

            var includes = projectLoadedModel.Item1.Project.GetReferencedProjects().ToList();
            var mismatches = includes.Select(reference => mismatchDetector.FindMismatch(projectReferences, projectLoadedModel.Item1.File.FileName, reference));

            foreach (var misMatch in mismatches)
            {
                projectLoadedModel.Item2.Add(misMatch);
            }
        }

        FindMismatchInIncludeReferences(options, loadedProjectWithModel);

        return Task.FromResult<INamedCollection>(target);
    }

    private void FindMismatchInIncludeReferences(IFolderFixOptions options, List<Tuple<ProjectLoadedModel, ProjectsReferencesToFixModel>> loadedProjects)
    {
        var projectReferences = new ProjectFileContainer();
        FileLocator.FindFilesWithoutBinFolder("*.props", options.ProjectReferences, projectReferences);
        FileLocator.FindFilesWithoutBinFolder("*.props", options.DirectoriesToInspect, projectReferences);

        foreach (var projectLoadedModel in loadedProjects)
        {
            var includes = projectLoadedModel.Item1.Project.GetIncludes().ToList();

            foreach (var reference in includes)
            {
                var mismatchResult = mismatchDetector.FindMismatch(projectReferences, projectLoadedModel.Item1.File.FileName, reference);
                projectLoadedModel.Item2.Add(mismatchResult);
            }

            var mismatches = includes.Select(reference => mismatchDetector.FindMismatch(projectReferences, projectLoadedModel.Item1.File.FileName, reference));

            foreach (var misMatch in mismatches)
            {
                projectLoadedModel.Item2.Add(misMatch);
            }
        }
    }
}