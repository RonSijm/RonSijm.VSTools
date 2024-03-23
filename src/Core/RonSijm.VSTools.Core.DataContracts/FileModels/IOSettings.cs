namespace RonSijm.VSTools.Core.DataContracts.FileModels;

public static class IOSettings
{
    public static readonly Func<string, bool> BuildFolderExclusion = folder =>
    {
        if (folder.EndsWith(@"\bin", StringComparison.InvariantCultureIgnoreCase) || folder.EndsWith(@"\obj", StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        if(folder.Contains(@"\bin\", StringComparison.InvariantCultureIgnoreCase) || folder.Contains(@"\obj\", StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }
        
        return false;
    };
}