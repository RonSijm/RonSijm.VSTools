using RonSijm.VSTools.Core.DataContracts.FileModels;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

namespace RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Helpers;

public static class ItemsToFixExtensions
{
    public static void Add<T>(this FixableCollectionBase<T> collection, OneOf<ErrorWhileMatchingResult, ProjectInProjectToFixResult>? item) where T : class, IReturnable
    {
        if (item is { IsT0: true })
        {
            collection.Add(item.Value.AsT0);
        }
        else if (item is { IsT1: true })
        {
            collection.Add(item.Value.AsT1 as T);
        }
    }

    public static void Add<T>(this FixableCollectionBase<T> collection, OneOf<ErrorWhileMatchingResult, T>? item) where T : IReturnable
    {
        if (item is { IsT0: true })
        {
            collection.Add(item.Value.AsT0);
        }
        else if (item is { IsT1: true })
        {
            collection.Add(item.Value.AsT1);
        }
    }
}