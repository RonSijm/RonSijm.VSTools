using Microsoft.Build.Construction;
using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.FileModels;

namespace RonSijm.VSTools.Core.FileOps;

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