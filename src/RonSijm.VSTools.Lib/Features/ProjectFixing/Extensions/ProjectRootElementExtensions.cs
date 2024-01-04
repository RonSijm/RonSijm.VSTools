using Microsoft.Build.Construction;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;

public static class ProjectRootElementExtensions
{
    public static IEnumerable<ProjectItemElement> GetReferencedProjects(this ProjectRootElement project)
    {
        foreach (var itemGroupElement in project.ItemGroups)
        {
            foreach (var itemChild in itemGroupElement.Children)
            {
                if (itemChild is not ProjectItemElement projectItemElement)
                {
                    continue;
                }

                if (projectItemElement.Include.EndsWith(".csproj", StringComparison.Ordinal))
                {
                    yield return projectItemElement;
                }
            }
        }
    }
}