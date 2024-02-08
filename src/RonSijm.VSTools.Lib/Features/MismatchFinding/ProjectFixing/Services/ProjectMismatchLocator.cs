namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class ProjectMismatchLocator(ProjectReferenceLoader projectReferenceLoader, MismatchDetector mismatchDetector) : IMismatchLocator
{
    public ushort Order => 2;

    /// <inheritdoc />
    public INamedCollection GetMismatches(CoreOptionsRequest options)
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

        return target;
    }

    private void FindMismatchInIncludeReferences(CoreOptionsRequest options, List<Tuple<ProjectLoadedModel, ProjectsReferencesToFixModel>> loadedProjects)
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