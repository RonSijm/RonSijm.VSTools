namespace RonSijm.VSTools.Lib.Features.Core;

public static class DirectoryLocator
{
    public static List<string> GetAllDirectoriesWithoutBuildFolders(string directoryPath)
    {
        return GetAllDirectories(directoryPath, IOSettings.BuildFolderExclusion);
    }

    public static List<string> GetAllDirectories(string directoryPath, Func<string, bool> excludeFolders)
    {
        var result = new List<string>();

        var queue = new Queue<string>();

        queue.Enqueue(directoryPath);

        while (queue.Count > 0)
        {
            var dir = queue.Dequeue();
            var subDirectoryString = dir.Replace(directoryPath, string.Empty, StringComparison.OrdinalIgnoreCase);

            result.Add(subDirectoryString);

            foreach (var subDirectory in Directory.GetDirectories(dir))
            {
                if (excludeFolders != null && excludeFolders(subDirectory))
                {
                    continue;
                }

                queue.Enqueue(subDirectory);
            }
        }

        return result;
    }
}