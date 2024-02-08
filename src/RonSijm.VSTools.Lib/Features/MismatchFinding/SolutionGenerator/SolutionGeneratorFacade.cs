using Microsoft.VisualStudio.SlnGen;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionGenerator;

public class SolutionGeneratorFacade : ICoreFunction
{
    public VSToolResult Run(CoreOptionsRequest options)
    {
        var allProjectReferences = new List<FileModel>();

        FileLocator.FindFilesWithoutBinFolder("*.csproj", options.ProjectReferences, allProjectReferences);
        FileLocator.FindFilesWithoutBinFolder("*.csproj", options.DirectoriesToInspect, allProjectReferences);

        var configurations = new[] { "Debug", "Release" };
        var platforms = new[] { "AnyCPU", "x64", "x86" };

        var slnFile = new SlnFile
        {
            Configurations = configurations,
            Platforms = platforms,
        };

        var projects = new List<SlnProject>();

        foreach (var allProjectReference in allProjectReferences)
        {
            var projectName = Path.GetFileName(allProjectReference.FileName)?.Replace(".csproj", string.Empty);

            var project = new SlnProject
            {
                Configurations = configurations,
                FullPath = allProjectReference.FileName,
                IsMainProject = true,
                Name = projectName,
                Platforms = platforms,
                ProjectGuid = Guid.NewGuid(),
                ProjectTypeGuid = Guid.NewGuid(),
            };

            projects.Add(project);
        }

        slnFile.AddProjects(projects);
        slnFile.Save("C:\\Dev\\RonSijm\\RonSijm.VSTools\\Sample\\GeneratedSolution.sln", useFolders: false, alwaysBuild: false);

        return new VSToolResult();
    }

    public ModeEnum Function => ModeEnum.CreateSolution;
}