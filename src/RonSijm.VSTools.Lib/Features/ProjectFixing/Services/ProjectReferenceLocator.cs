using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.FileLocating;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Abstractions;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Models;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Services;

public class ProjectReferenceLocator
{
    /// <summary>
    /// Gets the Mismatching References and returns them.
    /// This method does not actually fix them, for the purpose of allowing you to do a cold-run, and display what would be fixed before commiting to actually fixing it.
    /// </summary>
    /// <param name="directoriesToInspect">The project directories to inspect for Mismatching References</param>
    /// <param name="projectReferences">Other directories that might contain projects needed to be referenced. These projects themselves won't be fixed, but will just be used as references</param>
    public ProjectToFixResponse GetMismatchingReferences(List<string> directoriesToInspect, List<string> projectReferences)
    {
        var projectLoader = new ProjectLoader();
        var loadedProjects = projectLoader.LoadProjects(directoriesToInspect);

        var result = new ProjectToFixResponse();

        FindMismatchesInProjectReferences(directoriesToInspect, projectReferences, loadedProjects, result);
        FindMismatchInIncludeReferences(directoriesToInspect, projectReferences, loadedProjects, result);

        return result;
    }

    private static void FindMismatchInIncludeReferences(List<string> directoriesToInspect, List<string> projectReferences, List<ProjectRootElement> loadedProjects, ProjectToFixResponse result)
    {
        var allIncludeReferences = new List<string>();
        FileLocator.FindFiles("*.props", projectReferences, allIncludeReferences);
        FileLocator.FindFiles("*.props", directoriesToInspect, allIncludeReferences);

        foreach (var projectRootElement in loadedProjects)
        {
            var includes = projectRootElement.GetIncludes().ToList();
            FindMismatches(allIncludeReferences, includes, result, projectRootElement.FullPath);
        }
    }

    private static void FindMismatchesInProjectReferences(List<string> directoriesToInspect, List<string> projectReferences, List<ProjectRootElement> loadedProjects, ProjectToFixResponse result)
    {
        var allProjectReferences = new List<string>();
        FileLocator.FindFiles("*.csproj", projectReferences, allProjectReferences);
        FileLocator.FindFiles("*.csproj", directoriesToInspect, allProjectReferences);

        foreach (var projectRootElement in loadedProjects)
        {
            var references = projectRootElement.GetReferencedProjects().ToList();
            FindMismatches(allProjectReferences, references, result, projectRootElement.FullPath);
        }
    }

    private static void FindMismatches(List<string> projectReferences, List<ItemReference> references, ProjectToFixResponse result, string projectPath)
    {
        foreach (var reference in references)
        {
            var fileName = Path.GetFileName(reference.Path);

            if (fileName == null)
            {
                result.Errors.Add((reference, "Filename could not be determined"));
                continue;
            }

            var referencePath = projectReferences.SingleOrDefault(x => x.EndsWith(fileName, StringComparison.Ordinal));

            if (referencePath == null)
            {
                result.Errors.Add((reference, "Reference not found"));
                continue;
            }

            var expectedPath = Path.GetRelativePath(projectPath, referencePath)[3..];

            if (reference.Path != expectedPath)
            {
                result.ProjectsToFix.Add((reference, expectedPath));
            }
        }
    }
}