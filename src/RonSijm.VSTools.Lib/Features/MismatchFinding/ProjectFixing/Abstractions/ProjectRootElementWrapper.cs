namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Abstractions;

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