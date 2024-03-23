using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;

public interface IHaveInnerReturnablesOfT<T> : IHaveInnerReturnables where T : IReturnable
{
    IReadOnlyList<object> IHaveInnerReturnables.InnerItems => InnerItems?.Cast<object>().ToList();

    new IReadOnlyList<T> InnerItems { get; }

    public void Add(T item)
    {
        if (InnerItems is IList<T> isIList)
        {
            isIList.Add(item);
        }
    }

    public void AddRange(IEnumerable<T> items)
    {
        if (InnerItems is List<T> isList)
        {
            isList.AddRange(items);
        }
        else
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }
    }

    public void Remove(T item)
    {
        if (InnerItems is IList<T> isIList)
        {
            isIList.Remove(item);
        }
    }
}