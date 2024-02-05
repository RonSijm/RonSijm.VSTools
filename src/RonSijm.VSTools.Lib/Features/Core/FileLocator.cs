namespace RonSijm.VSTools.Lib.Features.Core;

public static class FileLocator
{
    public static void FindFiles<T>(string files, string inDirectory, params List<T>[] outputLists) where T : FileModel, new()
    {
        FindFiles(files, [inDirectory], outputLists);
    }

    public static void FindFiles<T>(string files, List<string> inDirectories, params List<T>[] outputLists) where T : FileModel, new()
    {
        if (inDirectories == null || inDirectories.Count == 0)
        {
            return;
        }

        foreach (var projectDirectory in inDirectories)
        {
            var csprojFiles = Directory.GetFiles(projectDirectory, files, System.IO.SearchOption.AllDirectories);

            foreach (var outputList in outputLists)
            {
                foreach (var csprojFile in csprojFiles)
                {
                    outputList.Add(new T { FileName = csprojFile });
                }
            }
        }
    }
}