using Microsoft.Build.Construction;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;

public static class ProjectElementContainerExtensions
{
    public static ProjectRootElement GetRoot(this ProjectElementContainer project)
    {
        if (project is ProjectRootElement rootElement)
        {
            return rootElement;
        }

        return GetRoot(project.Parent);
    }
}