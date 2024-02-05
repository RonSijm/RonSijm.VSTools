using System.Collections;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;

public class NamespaceChangedCollectionModel : IList<NamespaceChangeModel>
{
    public List<NamespaceChangeModel> Values { get; set; } = [];

    public void AddRange(List<NamespaceChangeModel> result)
    {
        Values.AddRange(result);
    }

    public void AddRange(NamespaceChangedCollectionModel renamedNamespaces)
    {
        Values.AddRange(renamedNamespaces);
    }

    public NamespaceChangedCollectionModel Rebuild()
    {
        var values = Values.Distinct().OrderByDescending(x => x.OldNamespace.Length).ToList();
        return new NamespaceChangedCollectionModel { Values = values };
    }

    #region IList Delegations
    public IEnumerator<NamespaceChangeModel> GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Values).GetEnumerator();
    }

    public void Add(NamespaceChangeModel item)
    {
        Values.Add(item);
    }

    public void Clear()
    {
        Values.Clear();
    }

    public bool Contains(NamespaceChangeModel item)
    {
        return Values.Contains(item);
    }

    public void CopyTo(NamespaceChangeModel[] array, int arrayIndex)
    {
        Values.CopyTo(array, arrayIndex);
    }

    public bool Remove(NamespaceChangeModel item)
    {
        return Values.Remove(item);
    }

    public int Count => Values.Count;

    public bool IsReadOnly => false;

    public int IndexOf(NamespaceChangeModel item)
    {
        return Values.IndexOf(item);
    }

    public void Insert(int index, NamespaceChangeModel item)
    {
        Values.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Values.RemoveAt(index);
    }

    public NamespaceChangeModel this[int index]
    {
        get => Values[index];
        set => Values[index] = value;
    }

    #endregion
}