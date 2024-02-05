namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class ProjectFileLoader
{
    public List<ProjectRootElement> OpenProjects(List<string> directoriesToInspect)
    {
        var projectsToFix = new List<FileModel>();
        FileLocator.FindFiles("*.csproj", directoriesToInspect, projectsToFix);

        var loadedProjects = new List<ProjectRootElement>();

        foreach (var project in projectsToFix)
        {
            var projectRootElement = ProjectRootElement.Open(project.FileName);
            loadedProjects.Add(projectRootElement);
        }

        return loadedProjects;
    }
}