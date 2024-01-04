using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.FileLocating;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Models;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Services;

public class ProjectReferenceLocator
{
    /// <summary>
    /// Gets the Mismatching References and returns them.
    /// This method does not actually fix them, for the purpose of allowing you to do a cold-run, and display what would be fixed before commiting to actually fixing it.
    /// </summary>
    /// <param name="projectDirectoriesToInspect">The project directories to inspect for Mismatching References</param>
    /// <param name="projectReferences">Other directories that might contain projects needed to be referenced. These projects themselves won't be fixed, but will just be used as references</param>
    public ProjectToFixResponse GetMismatchingReferences(List<string> projectDirectoriesToInspect, List<string> projectReferences)
    {
        var allProjects = new List<string>();
        FileLocator.FindFiles("*.csproj", projectReferences, allProjects);

        var projectsToFix = new List<string>();
        FileLocator.FindFiles("*.csproj", projectDirectoriesToInspect, allProjects, projectsToFix);

        allProjects = allProjects.Distinct().ToList();

        var includesToFix = GetIncludesToFix(projectsToFix, allProjects);

        return includesToFix;
    }

    private static ProjectToFixResponse GetIncludesToFix(List<string> projectDirectoriesToInspect, List<string> projectReferences)
    {
        var result = new ProjectToFixResponse();

        foreach (var project in projectDirectoriesToInspect)
        {
            var projectRootElement = ProjectRootElement.Open(project);
            var references = projectRootElement.GetReferencedProjects().ToList();

            foreach (var reference in references)
            {
                var fileName = Path.GetFileName(reference.Include);

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

                var expectedPath = Path.GetRelativePath(project, referencePath)[3..];

                if (reference.Include != expectedPath)
                {
                    result.ProjectsToFix.Add((reference, expectedPath));
                }
            }
        }

        return result;
    }
}