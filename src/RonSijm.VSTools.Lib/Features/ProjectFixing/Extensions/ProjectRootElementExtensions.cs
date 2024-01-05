using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Abstractions;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;

public static class ProjectRootElementExtensions
{
    public static IEnumerable<ItemReference> GetReferencedProjects(this ProjectRootElement project)
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

    public static IEnumerable<ItemReference> GetIncludes(this ProjectRootElement project)
    {
        foreach (var itemGroupElement in project.Imports)
        {
            yield return itemGroupElement;
        }
    }
}