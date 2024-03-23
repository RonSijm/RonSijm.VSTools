using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts;
using RonSijm.VSTools.Core.FileOps;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.Module.ReferenceGenerator;

public class ProjectReferenceMappingFacade(IAsyncLogger<ProjectReferenceMappingFacade> logger) : ICoreFunction<ProjectReferenceMappingOptions>
{
    public string FunctionDescription => "Finds projects, and gives projects a ReferenceId, and creates a cache of projects and their ReferenceIds.";

    public ModeEnum Function => ModeEnum.CreateReferences;

    public async Task<VSToolResult> Run(ProjectReferenceMappingOptions options)
    {
        var result = new VSToolResult();

        var projectLoader = new ProjectFileLoader();
        var loadedProjects = projectLoader.OpenProjects(options.DirectoriesToInspect);

        var projectReferenceLocator = new ProjectFromSolutionLocator();
        var projectReferences = projectReferenceLocator.GetProjectReferences(loadedProjects);

        result.InitResult = projectReferences;

        if (projectReferences.Any())
        {
            await logger.LogInformation("Project References:");
        }

        foreach (var projectReferenceModel in projectReferences)
        {
            var project = Path.GetFileName(projectReferenceModel.Project.FullPath);

            await logger.LogInformation("Project: {Project} - Existing: {Existing} - ReferenceId: {ProjectReferenceId}", project, projectReferenceModel.Existing, projectReferenceModel.ProjectReferenceId);
        }

        if (options.RealRun != RunModeEnum.Real)
        {
            return result;
        }

        var projectFileReferenceModifier = new ProjectFileReferenceModifier();
        projectFileReferenceModifier.AddReferenceToProjects(projectReferences);

        var projectReferenceToOptionsFactory = new ProjectReferenceToOptionsFactory();

#warning : Save results
        //projectReferenceToOptionsFactory.SaveReferencesToOptions(options, projectReferences);

        return result;
    }
}