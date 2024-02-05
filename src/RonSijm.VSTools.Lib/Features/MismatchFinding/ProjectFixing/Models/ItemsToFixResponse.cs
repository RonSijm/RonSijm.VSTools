namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ItemsToFixResponse : IFixable
{
    public bool HasItems { get; private set; }

    public IReadOnlyList<IFixableItem> ItemsToFix => _itemsToFix;
    private readonly List<IFixableItem> _itemsToFix = [];

    public IReadOnlyList<ErrorWhileMatchingResult> Errors => _errors;
    private readonly List<ErrorWhileMatchingResult> _errors = [];


    public void Add(ErrorWhileMatchingResult error)
    {
        HasItems = true;
        _errors.Add(error);
    }

    public void Add(IFixableItem item)
    {
        HasItems = true;
        _itemsToFix.Add(item);
    }

    public void AddRange<T>(List<T> items) where T : IFixableItem
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

    public void Remove(IFixableItem error)
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