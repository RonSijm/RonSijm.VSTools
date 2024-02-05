namespace RonSijm.VSTools.Lib.Features.Core;

public static class FileLocator
{
    public static void FindFilesWithoutBinFolder<T>(string files, string inDirectory, params List<T>[] outputLists) where T : FileModel, new()
    {
        FindFiles(files, [inDirectory], IOSettings.BuildFolders, outputLists);
    }

    public static void FindFiles<T>(string files, string inDirectory, List<string> excludeFolders, params List<T>[] outputLists) where T : FileModel, new()
    {
        FindFiles(files, [inDirectory], excludeFolders, outputLists);
    }

    public static void FindFilesWithoutBinFolder<T>(string files, List<string> inDirectories, params List<T>[] outputLists) where T : FileModel, new()
    {
        FindFiles(files, inDirectories, IOSettings.BuildFolders, outputLists);
    }

    public static void FindFiles<T>(string files, List<string> inDirectories, List<string> excludeFolders, params List<T>[] outputLists) where T : FileModel, new()
    {
        if (inDirectories == null || inDirectories.Count == 0)
        {
            return;
        }

        foreach (var projectDirectory in inDirectories)
        {
            var foundFiles = Directory.GetFiles(projectDirectory, files, System.IO.SearchOption.AllDirectories);

            if (excludeFolders != null && excludeFolders.Count != 0)
            {
                foundFiles = foundFiles.Where(file => !excludeFolders.Any(excludeFolder => file.Contains(excludeFolder, StringComparison.InvariantCultureIgnoreCase))).ToArray();
            }

            foreach (var outputList in outputLists)
            {
                foreach (var csprojFile in foundFiles)
                {
                    outputList.Add(new T { FileName = csprojFile });
                }
            }
        }
    }
}