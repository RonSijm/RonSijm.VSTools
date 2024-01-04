namespace RonSijm.VSTools.Lib.Features.FileLocating;

public static class FileLocator
{
    public static void FindFiles(string files, List<string> inDirectories, params List<string>[] outputLists)
    {
        foreach (var projectDirectory in inDirectories)
        {
            var csprojFiles = Directory.GetFiles(projectDirectory, files, SearchOption.AllDirectories);

            foreach (var outputList in outputLists)
            {
                outputList.AddRange(csprojFiles);
            }
        }
    }
}