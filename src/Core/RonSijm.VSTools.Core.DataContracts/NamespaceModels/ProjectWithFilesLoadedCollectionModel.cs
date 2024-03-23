namespace RonSijm.VSTools.Core.DataContracts.NamespaceModels;

public class ProjectWithFilesLoadedCollectionModel : IList<ProjectWithFilesLoadedModel>
{
    public List<ProjectWithFilesLoadedModel> Values { get; set; }

    public IEnumerator<ProjectWithFilesLoadedModel> GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    #region IList Delegations

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Values).GetEnumerator();
    }

    public void Add(ProjectWithFilesLoadedModel item)
    {
        Values.Add(item);
    }

    public void Clear()
    {
        Values.Clear();
    }

    public bool Contains(ProjectWithFilesLoadedModel item)
    {
        return Values.Contains(item);
    }

    public void CopyTo(ProjectWithFilesLoadedModel[] array, int arrayIndex)
    {
        Values.CopyTo(array, arrayIndex);
    }

    public bool Remove(ProjectWithFilesLoadedModel item)
    {
        return Values.Remove(item);
    }

    public int Count => Values.Count;

    public bool IsReadOnly => false;

    public int IndexOf(ProjectWithFilesLoadedModel item)
    {
        return Values.IndexOf(item);
    }

    public void Insert(int index, ProjectWithFilesLoadedModel item)
    {
        Values.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Values.RemoveAt(index);
    }

    public ProjectWithFilesLoadedModel this[int index]
    {
        get => Values[index];
        set => Values[index] = value;
    }

    #endregion
}