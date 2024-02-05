namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public abstract class ItemsToFixResponse<T> : IFixable where T : IFixableItem
{
    public bool HasItems { get; private set; }

    public IReadOnlyList<T> ItemsToFix => _itemsToFix;
    private readonly List<T> _itemsToFix = [];

    public IReadOnlyList<ErrorWhileMatchingResult> Errors => _errors;
    private readonly List<ErrorWhileMatchingResult> _errors = [];


    public void Add(ErrorWhileMatchingResult error)
    {
        HasItems = true;
        _errors.Add(error);
    }

    public void Add(T item)
    {
        HasItems = true;
        _itemsToFix.Add(item);
    }

    public void AddRange<TItem>(List<TItem> items) where TItem : T
    {
        if (items == null || items.Count == 0)
        {
            return;
        }

        HasItems = true;

        foreach (var fixableItem in items)
        {

            _itemsToFix.Add(fixableItem);
        }

    }

    public void Remove(T error)
    {
        _itemsToFix.Remove(error);
        HasItems = _itemsToFix.Count != 0;
    }

    public void Fix()
    {
        foreach (var item in ItemsToFix)
        {
            item.Fix();
        }
    }
}