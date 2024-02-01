using RonSijm.VSTools.Lib.Features.Core;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class ProjectLoader
{
    public List<ProjectRootElement> LoadProjects(List<string> directoriesToInspect)
    {
        var projectsToFix = new List<ProjectFileModel>();
        FileLocator.FindFiles("*.csproj", directoriesToInspect, projectsToFix);

        var loadedProjects = new List<ProjectRootElement>();

        foreach (var project in projectsToFix)
        {
            var projectRootElement = ProjectRootElement.Open(project.File);
            loadedProjects.Add(projectRootElement);
        }

        return loadedProjects;
    }
}