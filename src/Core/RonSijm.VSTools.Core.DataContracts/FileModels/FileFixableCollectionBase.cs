using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

namespace RonSijm.VSTools.Core.DataContracts.FileModels;

public abstract class FileFixableCollectionBase<T> : FixableCollectionBase<T> where T : IReturnable
{
    public FileModel FileModel { get; set; }
}