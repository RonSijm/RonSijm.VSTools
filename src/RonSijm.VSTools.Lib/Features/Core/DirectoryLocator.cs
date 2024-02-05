namespace RonSijm.VSTools.Lib.Features.Core;

public static class DirectoryLocator
{
    public static List<string> GetAllDirectoriesWithoutBuildFolders(string directoryPath)
    {
        return GetAllDirectories(directoryPath, IOSettings.BuildFolders);
    }

    public static List<string> GetAllDirectories(string directoryPath, List<string> excludeFolders)
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
                if (excludeFolders != null && excludeFolders.Any(subDirectory.Contains))
                {
                    continue;
                }

                queue.Enqueue(subDirectory);
            }
        }

        return result;
    }
}