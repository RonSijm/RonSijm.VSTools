using Microsoft.Build.Construction;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Abstractions;

public class ProjectRootElementWrapper(ProjectRootElement projectRootElement) : ISaveable
{
    public void Save()
    {
        projectRootElement.Save();
    }

    public static implicit operator ProjectRootElementWrapper(ProjectRootElement projectItemElement)
    {
        return new ProjectRootElementWrapper(projectItemElement);
    }
}