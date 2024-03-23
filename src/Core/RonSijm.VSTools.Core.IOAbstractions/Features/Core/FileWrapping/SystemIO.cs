namespace RonSijm.VSTools.Core.IOAbstractions.Features.Core.FileWrapping;

public static class SystemIO
{
    public static IDirectory Directory => new DirectoryWrapper();

    public static IFile File => new FileWrapper();
    
    public static IPath Path => new PathWrapper();
}