namespace RonSijm.VSTools.Lib.Features.Core.Models;

public abstract class FileFixableCollectionBase<T> : FixableCollectionBase<T> where T : IReturnable
{
    public FileModel FileModel { get; set; }
}