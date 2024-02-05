// ReSharper disable NotNullOrRequiredMemberIsNotInitialized
// ReSharper disable UnassignedGetOnlyAutoProperty
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace RonSijm.VSTools.Lib.Features.Core.FileWrapping;

public class FileWrapper : IFile
{
    public IFileSystem FileSystem { get; }
    public async Task AppendAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task AppendAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = new CancellationToken())

    {
        throw new NotImplementedException();
    }

    public async Task AppendAllTextAsync(string path, string contents, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task AppendAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task<string[]> ReadAllLinesAsync(string path, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task<string> ReadAllTextAsync(string path, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task<string> ReadAllTextAsync(string path, Encoding encoding, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<string> ReadLinesAsync(string path, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<string> ReadLinesAsync(string path, Encoding encoding, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task WriteAllTextAsync(string path, string contents, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public async Task WriteAllTextAsync(string path, string contents, Encoding encoding, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new NotImplementedException();
    }

    public void AppendAllLines(string path, IEnumerable<string> contents)
    {
        throw new NotImplementedException();
    }

    public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
        throw new NotImplementedException();
    }

    public void AppendAllText(string path, string contents)
    {
        throw new NotImplementedException();
    }

    public void AppendAllText(string path, string contents, Encoding encoding)
    {
        throw new NotImplementedException();
    }

    public StreamWriter AppendText(string path)
    {
        throw new NotImplementedException();
    }

    public void Copy(string sourceFileName, string destFileName)
    {
        throw new NotImplementedException();
    }

    public void Copy(string sourceFileName, string destFileName, bool overwrite)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream Create(string path)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream Create(string path, int bufferSize)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream Create(string path, int bufferSize, FileOptions options)
    {
        throw new NotImplementedException();
    }

    public IFileSystemInfo CreateSymbolicLink(string path, string pathToTarget)
    {
        throw new NotImplementedException();
    }

    public StreamWriter CreateText(string path)
    {
        throw new NotImplementedException();
    }

    public void Decrypt(string path)
    {
        throw new NotImplementedException();
    }

    public void Delete(string path)
    {
        throw new NotImplementedException();
    }

    public void Encrypt(string path)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string path)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.File.Exists(path) : IOThrowHelper.ReturnOrThrowRead<bool>();
    }

    public FileAttributes GetAttributes(string path)
    {
        throw new NotImplementedException();
    }

    public FileAttributes GetAttributes(SafeFileHandle fileHandle)
    {
        throw new NotImplementedException();
    }

    public DateTime GetCreationTime(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetCreationTime(SafeFileHandle fileHandle)
    {
        throw new NotImplementedException();
    }

    public DateTime GetCreationTimeUtc(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetCreationTimeUtc(SafeFileHandle fileHandle)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastAccessTime(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastAccessTime(SafeFileHandle fileHandle)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastAccessTimeUtc(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastAccessTimeUtc(SafeFileHandle fileHandle)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastWriteTime(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastWriteTime(SafeFileHandle fileHandle)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastWriteTimeUtc(string path)
    {
        throw new NotImplementedException();
    }

    public DateTime GetLastWriteTimeUtc(SafeFileHandle fileHandle)
    {
        throw new NotImplementedException();
    }

    public UnixFileMode GetUnixFileMode(string path)
    {
        throw new NotImplementedException();
    }

    public UnixFileMode GetUnixFileMode(SafeFileHandle fileHandle)
    {
        throw new NotImplementedException();
    }

    public void Move(string sourceFileName, string destFileName)
    {
        if (IOThrowHelper.WriteEnabled)
        {
            System.IO.File.Move(sourceFileName, destFileName);
        }
        else
        {
            IOThrowHelper.IgnoreOrThrowWrite();
        }
    }

    public void Move(string sourceFileName, string destFileName, bool overwrite)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream Open(string path, FileMode mode)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream Open(string path, FileMode mode, FileAccess access)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream Open(string path, FileStreamOptions options)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream OpenRead(string path)
    {
        throw new NotImplementedException();
    }

    public StreamReader OpenText(string path)
    {
        throw new NotImplementedException();
    }

    public FileSystemStream OpenWrite(string path)
    {
        throw new NotImplementedException();
    }

    public byte[] ReadAllBytes(string path)
    {
        throw new NotImplementedException();
    }

    public string[] ReadAllLines(string path)
    {
        throw new NotImplementedException();
    }

    public string[] ReadAllLines(string path, Encoding encoding)
    {
        throw new NotImplementedException();
    }

    public string ReadAllText(string path)
    {
        return IOThrowHelper.ReadEnabled ? System.IO.File.ReadAllText(path) : IOThrowHelper.ReturnOrThrowRead<string>();
    }

    public string ReadAllText(string path, Encoding encoding)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> ReadLines(string path)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> ReadLines(string path, Encoding encoding)
    {
        throw new NotImplementedException();
    }

    public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
    {
        throw new NotImplementedException();
    }

    public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
        throw new NotImplementedException();
    }

    public IFileSystemInfo ResolveLinkTarget(string linkPath, bool returnFinalTarget)
    {
        throw new NotImplementedException();
    }

    public void SetAttributes(string path, FileAttributes fileAttributes)
    {
        throw new NotImplementedException();
    }

    public void SetAttributes(SafeFileHandle fileHandle, FileAttributes fileAttributes)
    {
        throw new NotImplementedException();
    }

    public void SetCreationTime(string path, DateTime creationTime)
    {
        throw new NotImplementedException();
    }

    public void SetCreationTime(SafeFileHandle fileHandle, DateTime creationTime)
    {
        throw new NotImplementedException();
    }

    public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
        throw new NotImplementedException();
    }

    public void SetCreationTimeUtc(SafeFileHandle fileHandle, DateTime creationTimeUtc)
    {
        throw new NotImplementedException();
    }

    public void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
        throw new NotImplementedException();
    }

    public void SetLastAccessTime(SafeFileHandle fileHandle, DateTime lastAccessTime)
    {
        throw new NotImplementedException();
    }

    public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
        throw new NotImplementedException();
    }

    public void SetLastAccessTimeUtc(SafeFileHandle fileHandle, DateTime lastAccessTimeUtc)
    {
        throw new NotImplementedException();
    }

    public void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
        throw new NotImplementedException();
    }

    public void SetLastWriteTime(SafeFileHandle fileHandle, DateTime lastWriteTime)
    {
        throw new NotImplementedException();
    }

    public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
        throw new NotImplementedException();
    }

    public void SetLastWriteTimeUtc(SafeFileHandle fileHandle, DateTime lastWriteTimeUtc)
    {
        throw new NotImplementedException();
    }

    public void SetUnixFileMode(string path, UnixFileMode mode)
    {
        throw new NotImplementedException();
    }

    public void SetUnixFileMode(SafeFileHandle fileHandle, UnixFileMode mode)
    {
        throw new NotImplementedException();
    }

    public void WriteAllBytes(string path, byte[] bytes)
    {
        throw new NotImplementedException();
    }

    public void WriteAllLines(string path, string[] contents)
    {
        throw new NotImplementedException();
    }

    public void WriteAllLines(string path, IEnumerable<string> contents)
    {
        throw new NotImplementedException();
    }

    public void WriteAllLines(string path, string[] contents, Encoding encoding)
    {
        throw new NotImplementedException();
    }

    public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
    {
        throw new NotImplementedException();
    }

    public void WriteAllText(string path, string contents)
    {
        if (IOThrowHelper.WriteEnabled)
        {
            System.IO.File.WriteAllText(path, contents);
        }
        else
        {
            IOThrowHelper.IgnoreOrThrowWrite();
        }
    }

    public void WriteAllText(string path, string contents, Encoding encoding)
    {
        throw new NotImplementedException();
    }
}