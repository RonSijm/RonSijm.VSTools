using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.FileLocating;
using RonSijm.VSTools.Lib.Features.SolutionFixing.Models;

namespace RonSijm.VSTools.Lib.Features.SolutionFixing.Services;

public class SolutionReferenceLocator
{
    public List<SolutionToFixModel> GetMismatchingReferences(List<string> solutionDirectoriesToInspect, List<string> projectReferences)
    {
        var results = new List<SolutionToFixModel>();

        var solutions = GetSolutionFiles(solutionDirectoriesToInspect);
        var allProjects = GetProjectFiles(solutionDirectoriesToInspect, projectReferences);

        foreach (var solution in solutions)
        {
            SolutionToFixModel result = null;

            var solutionFile = SolutionFile.Parse(solution);

            var projectItems = solutionFile.ProjectsInOrder;

            foreach (var project in projectItems)
            {
                if (project.ProjectType == SolutionProjectType.SolutionFolder)
                {
                    continue;
                }
                else if (project.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat)
                {
                    var projectPath = project.AbsolutePath;
                    var fileName = Path.GetFileName(projectPath);

                    if (fileName == null)
                    {
                        result ??= new SolutionToFixModel(solution);
                        result.Errors.Add((project, "Filename could not be determined"));
                        continue;
                    }

                    var referencePath = allProjects.SingleOrDefault(x => x.EndsWith(fileName, StringComparison.Ordinal));

                    if (referencePath == null)
                    {
                        result ??= new SolutionToFixModel(solution);
                        result.Errors.Add((project, "Reference not found"));
                        continue;
                    }

                    if (project.AbsolutePath != referencePath)
                    {
                        result ??= new SolutionToFixModel(solution);
                        var expectedPath = Path.GetRelativePath(solution, referencePath)[3..];

                        result.ProjectsToFix.Add((project, expectedPath));
                    }
                }
                else
                {
                    // TODO
                }
            }

            if (result != null)
            {
                results.Add(result);
            }
        }

        return results;
    }

    private static List<string> GetSolutionFiles(List<string> projectReferences)
    {
        var solutions = new List<string>();
        FileLocator.FindFiles("*.sln", projectReferences, solutions);
        return solutions;
    }

    private static List<string> GetProjectFiles(List<string> solutionDirectoriesToInspect, List<string> projectReferences)
    {
        var allProjects = new List<string>();
        FileLocator.FindFiles("*.csproj", projectReferences, allProjects);
        FileLocator.FindFiles("*.csproj", solutionDirectoriesToInspect, allProjects);

        allProjects = allProjects.Distinct().ToList();
        return allProjects;
    }
}