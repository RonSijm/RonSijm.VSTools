using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Abstractions;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;

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