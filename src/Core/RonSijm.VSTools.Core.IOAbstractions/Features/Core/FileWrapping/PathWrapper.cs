// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace RonSijm.VSTools.Core.IOAbstractions.Features.Core.FileWrapping;

internal class PathWrapper : IPath
{
    public IFileSystem FileSystem { get; }
    public string ChangeExtension(string path, string extension)
    {
        throw new NotImplementedException();
    }

    public string Combine(string path1, string path2)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.Path.Combine(path1, path2) : IOThrowHelper.ReturnOrThrowRead<string>();
    }

    public string Combine(string path1, string path2, string path3)
    {
        throw new NotImplementedException();
    }

    public string Combine(string path1, string path2, string path3, string path4)
    {
        throw new NotImplementedException();
    }

    public string Combine(params string[] paths)
    {
        throw new NotImplementedException();
    }

    public bool EndsInDirectorySeparator(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public bool EndsInDirectorySeparator(string path)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string path)
    {
        throw new NotImplementedException();
    }

    public ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public string GetDirectoryName(string path)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.Path.GetDirectoryName(path) : IOThrowHelper.ReturnOrThrowRead<string>();
    }

    public ReadOnlySpan<char> GetExtension(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public string GetExtension(string path)
    {
        throw new NotImplementedException();
    }

    public ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public string GetFileName(string path)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.Path.GetFileName(path) : IOThrowHelper.ReturnOrThrowRead<string>();
    }

    public ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public string GetFileNameWithoutExtension(string path)
    {
        throw new NotImplementedException();
    }

    public string GetFullPath(string path)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.Path.GetFullPath(path) : IOThrowHelper.ReturnOrThrowRead<string>();
    }

    public string GetFullPath(string path, string basePath)
    {
        throw new NotImplementedException();
    }

    public char[] GetInvalidFileNameChars()
    {
        throw new NotImplementedException();
    }

    public char[] GetInvalidPathChars()
    {
        throw new NotImplementedException();
    }

    public ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public string GetPathRoot(string path)
    {
        throw new NotImplementedException();
    }

    public string GetRandomFileName()
    {
        throw new NotImplementedException();
    }

    public string GetRelativePath(string relativeTo, string path)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.Path.GetRelativePath(relativeTo, path) : IOThrowHelper.ReturnOrThrowRead<string>();
    }

    public string GetTempFileName()
    {
        throw new NotImplementedException();
    }

    public string GetTempPath()
    {
        throw new NotImplementedException();
    }

    public bool HasExtension(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public bool HasExtension(string path)
    {
        throw new NotImplementedException();
    }

    public bool IsPathFullyQualified(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public bool IsPathFullyQualified(string path)
    {
        throw new NotImplementedException();
    }

    public bool IsPathRooted(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public bool IsPathRooted(string path)
    {
        throw new NotImplementedException();
    }

    public string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)
    {
        throw new NotImplementedException();
    }

    public string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3)
    {
        throw new NotImplementedException();
    }

    public bool TryJoin(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, Span<char> destination, out int charsWritten)
    {
        throw new NotImplementedException();
    }

    public bool TryJoin(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, Span<char> destination, out int charsWritten)
    {
        throw new NotImplementedException();
    }

    public string Join(string path1, string path2)
    {
        throw new NotImplementedException();
    }

    public string Join(string path1, string path2, string path3)
    {
        throw new NotImplementedException();
    }

    public string Join(params string[] paths)
    {
        throw new NotImplementedException();
    }

    public ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path)
    {
        throw new NotImplementedException();
    }

    public string TrimEndingDirectorySeparator(string path)
    {
        throw new NotImplementedException();
    }

    public string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, ReadOnlySpan<char> path4)
    {
        throw new NotImplementedException();
    }

    public string Join(string path1, string path2, string path3, string path4)
    {
        throw new NotImplementedException();
    }

    public char AltDirectorySeparatorChar => System.IO.Path.AltDirectorySeparatorChar;
    public char DirectorySeparatorChar => System.IO.Path.DirectorySeparatorChar;
    public char PathSeparator => System.IO.Path.PathSeparator;
    public char VolumeSeparatorChar => System.IO.Path.VolumeSeparatorChar;
}