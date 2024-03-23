using Microsoft.VisualStudio.SlnGen;
using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.FileModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts;
using RonSijm.VSTools.Core.FileOps;

namespace RonSijm.VSTools.Module.SolutionGenerator;

public class SolutionGeneratorFacade : ICoreFunction<SolutionGeneratorOptions>
{
    public string FunctionDescription => "Finds projects in a given location, and creates a solution file for those projects.";

    public ModeEnum Function => ModeEnum.CreateSolution;
    public Task<VSToolResult> Run(SolutionGeneratorOptions options)
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
        slnFile.Save("C:\\Dev\\Appical\\GeneratedSolution.sln", useFolders: false, alwaysBuild: false);

        return Task.FromResult(new VSToolResult());
    }
}