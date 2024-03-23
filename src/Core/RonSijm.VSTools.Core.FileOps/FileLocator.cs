using RonSijm.VSTools.Core.DataContracts.FileModels;

namespace RonSijm.VSTools.Core.FileOps;

public static class FileLocator
{
    public static List<FileModel> FindFilesWithoutBinFolder(string files, string inDirectory)
    {
        var result = new List<FileModel>();
        FindFiles(files, [inDirectory], IOSettings.BuildFolderExclusion, result);

        return result;
    }

    public static void FindFilesWithoutBinFolder<T>(string files, string inDirectory, params List<T>[] outputLists) where T : FileModel, new()
    {
        FindFiles(files, [inDirectory], IOSettings.BuildFolderExclusion, outputLists);
    }


    public static void FindFilesWithoutBinFolder<T>(string files, List<string> inDirectories, params List<T>[] outputLists) where T : FileModel, new()
    {
        FindFiles(files, inDirectories, IOSettings.BuildFolderExclusion, outputLists);
    }

    public static void FindFiles<T>(string files, List<string> inDirectories, Func<string, bool> excludeFolders, params List<T>[] outputLists) where T : FileModel, new()
    {
        if (inDirectories == null || inDirectories.Count == 0)
        {
            return;
        }

        foreach (var projectDirectory in inDirectories)
        {
            var foundFiles = Directory.GetFiles(projectDirectory, files, System.IO.SearchOption.AllDirectories);

            if (excludeFolders != null)
            {
                foundFiles = foundFiles.Where(file => !excludeFolders(file)).ToArray();
            }

            foreach (var outputList in outputLists)
            {
                foreach (var csprojFile in foundFiles)
                {
                    outputList.Add(new T
                    {
                        FileName = csprojFile,
                        LoadedFromDirectory = projectDirectory,
                    });
                }
            }
        }
    }
}