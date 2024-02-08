namespace RonSijm.VSTools.Lib.Features.CreateReferences;

public class ProjectReferenceMappingFacade(ILogger<ProjectReferenceMappingFacade> logger) : ICoreFunction
{
    public ModeEnum Function => ModeEnum.CreateReferences;

    public VSToolResult Run(CoreOptionsRequest options)
    {
        var result = new VSToolResult();

        var projectLoader = new ProjectFileLoader();
        var loadedProjects = projectLoader.OpenProjects(options.DirectoriesToInspect);

        var projectReferenceLocator = new ProjectFromSolutionLocator();
        var projectReferences = projectReferenceLocator.GetProjectReferences(loadedProjects);

        result.InitResult = projectReferences;

        if (projectReferences.Any())
        {
            logger.LogInformation("Project References:");
        }

        foreach (var projectReferenceModel in projectReferences)
        {
            var project = Path.GetFileName(projectReferenceModel.Project.FullPath);

            logger.LogInformation("Project: {Project} - Existing: {Existing} - ReferenceId: {ProjectReferenceId}", project, projectReferenceModel.Existing, projectReferenceModel.ProjectReferenceId);
        }

        if (!options.RealRun)
        {
            return result;
        }

        var projectFileReferenceModifier = new ProjectFileReferenceModifier();
        projectFileReferenceModifier.AddReferenceToProjects(projectReferences);

        var projectReferenceToOptionsFactory = new ProjectReferenceToOptionsFactory();
        projectReferenceToOptionsFactory.SaveReferencesToOptions(options, projectReferences);

        return result;
    }
}