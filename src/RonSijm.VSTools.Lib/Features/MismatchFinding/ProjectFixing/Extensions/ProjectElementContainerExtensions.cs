namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Extensions;

public static class ProjectElementContainerExtensions
{
    public static ProjectRootElementWrapper GetRoot(this ProjectElementContainer project)
    {
        if (project is ProjectRootElement rootElement)
        {
            return rootElement;
        }

        return GetRoot(project.Parent);
    }
}