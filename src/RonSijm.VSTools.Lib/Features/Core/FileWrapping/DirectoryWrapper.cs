﻿// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace RonSijm.VSTools.Lib.Features.Core.FileWrapping;

internal class DirectoryWrapper : IDirectory
{
    public IFileSystem FileSystem { get; }


    public IDirectoryInfo CreateDirectory(string path)
    {
        throw new NotImplementedException();
    }

    public IDirectoryInfo CreateDirectory(string path, System.IO.UnixFileMode unixCreateMode)
    {
        throw new NotImplementedException();
    }

    public IFileSystemInfo CreateSymbolicLink(string path, string pathToTarget)
    {
        throw new NotImplementedException();
    }

    public IDirectoryInfo CreateTempSubdirectory(string prefix = null)
    {
        throw new NotImplementedException();
    }

    public void Delete(string path)
    {
        throw new NotImplementedException();
    }

    public void Delete(string path, bool recursive)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateDirectories(string path)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, System.IO.SearchOption searchOption)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateDirectories(string path, string searchPattern, System.IO.EnumerationOptions enumerationOptions)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFiles(string path)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFiles(string path, string searchPattern, System.IO.SearchOption searchOption)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFiles(string path, string searchPattern, System.IO.EnumerationOptions enumerationOptions)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, System.IO.SearchOption searchOption)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, System.IO.EnumerationOptions enumerationOptions)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetCreationTime(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetCreationTimeUtc(string path)
    {
        throw new NotImplementedException();
    }

    public string GetCurrentDirectory()
    {
        throw new NotImplementedException();
    }

    public string[] GetDirectories(string path)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.Directory.GetDirectories(path) : IOThrowHelper.ReturnOrThrowRead<string[]>();
    }

    public string[] GetDirectories(string path, string searchPattern)
    {
        throw new NotImplementedException();
    }

    public string[] GetDirectories(string path, string searchPattern, System.IO.SearchOption searchOption)
    {
        throw new NotImplementedException();
    }

    public string[] GetDirectories(string path, string searchPattern, System.IO.EnumerationOptions enumerationOptions)
    {
        throw new NotImplementedException();
    }

    public string GetDirectoryRoot(string path)
    {
        throw new NotImplementedException();
    }

    public string[] GetFiles(string path)
    {
        throw new NotImplementedException();
    }

    public string[] GetFiles(string path, string searchPattern)
    {
        throw new NotImplementedException();
    }

    public string[] GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.Directory.GetFiles(path, searchPattern, searchOption) : IOThrowHelper.ReturnOrThrowRead<string[]>();
    }

    public string[] GetFiles(string path, string searchPattern, System.IO.EnumerationOptions enumerationOptions)
    {
        throw new NotImplementedException();
    }

    public string[] GetFileSystemEntries(string path)
    {
        throw new NotImplementedException();
    }

    public string[] GetFileSystemEntries(string path, string searchPattern)
    {
        throw new NotImplementedException();
    }

    public string[] GetFileSystemEntries(string path, string searchPattern, System.IO.SearchOption searchOption)
    {
        throw new NotImplementedException();
    }

    public string[] GetFileSystemEntries(string path, string searchPattern, System.IO.EnumerationOptions enumerationOptions)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastAccessTime(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastAccessTimeUtc(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastWriteTime(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastWriteTimeUtc(string path)
    {
        throw new NotImplementedException();
    }

    public string[] GetLogicalDrives()
    {
        throw new NotImplementedException();
    }

    public IDirectoryInfo GetParent(string path)
    {
        throw new NotImplementedException();
    }

    public void Move(string sourceDirName, string destDirName)
    {
        throw new NotImplementedException();
    }

    public IFileSystemInfo ResolveLinkTarget(string linkPath, bool returnFinalTarget)
    {
        throw new NotImplementedException();
    }

    public void SetCreationTime(string path, DateTime creationTime)
    {
        throw new NotImplementedException();
    }

    public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
        throw new NotImplementedException();
    }

    public void SetCurrentDirectory(string path)
    {
        throw new NotImplementedException();
    }

    public void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
        throw new NotImplementedException();
    }

    public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
        throw new NotImplementedException();
    }

    public void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
        throw new NotImplementedException();
    }

    public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
        throw new NotImplementedException();
    }
}