namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class ProjectFileLoader
{
    public List<ProjectLoadedModel> OpenProjects(List<string> directoriesToInspect)
    {
        var projectsToFix = new List<FileModel>();
        FileLocator.FindFilesWithoutBinFolder("*.csproj", directoriesToInspect, projectsToFix);

        var loadedProjects = new List<ProjectLoadedModel>();

        foreach (var project in projectsToFix)
        {
            var projectRootElement = ProjectRootElement.Open(project.FileName);
            loadedProjects.Add(new ProjectLoadedModel(projectRootElement, project));
        }

        return loadedProjects;
    }
}