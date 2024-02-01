using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

namespace RonSijm.VSTools.Lib.Features.Core;

public static class FileLocator
{
    public static void FindFiles(string files, List<string> inDirectories, params List<ProjectFileModel>[] outputLists)
    {
        if (inDirectories == null || inDirectories.Count == 0)
        {
            return;
        }

        foreach (var projectDirectory in inDirectories)
        {
            var csprojFiles = Directory.GetFiles(projectDirectory, files, SearchOption.AllDirectories);

            foreach (var outputList in outputLists)
            {
                foreach (var csprojFile in csprojFiles)
                {
                    outputList.Add(new ProjectFileModel { File = csprojFile });
                }
            }
        }
    }
}