using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.FileLocating;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Services;

public class ProjectLoader
{
    public List<ProjectRootElement> LoadProjects(List<string> directoriesToInspect)
    {
        var projectsToFix = new List<string>();
        FileLocator.FindFiles("*.csproj", directoriesToInspect, projectsToFix);

        var loadedProjects = new List<ProjectRootElement>();

        foreach (var project in projectsToFix)
        {
            var projectRootElement = ProjectRootElement.Open(project);
            loadedProjects.Add(projectRootElement);
        }

        return loadedProjects;
    }
}