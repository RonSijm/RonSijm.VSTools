namespace RonSijm.VSTools.Lib.Features.Core.Models;

public abstract class FixableCollectionBase<T> : INamedCollection, ICanHaveErrors, IFixable, IHaveInnerReturnablesOfT<T> where T : IReturnable
{
    public int RelevantItemCount => this.GetCount();

    public bool HasRelevantItems { get; private set; }

    public IReadOnlyList<T> InnerItems => _itemsToFix;
    private readonly List<T> _itemsToFix = [];

    public virtual string ObjectName { get; set; }

    public IReadOnlyList<ErrorWhileMatchingResult> Errors => _errors;
    private readonly List<ErrorWhileMatchingResult> _errors = [];


    public void Add(ErrorWhileMatchingResult error)
    {
        HasRelevantItems = true;
        _errors.Add(error);
    }

    public void Add(T item)
    {
        HasRelevantItems = true;
        _itemsToFix.Add(item);
    }

    public void AddRange<TItem>(IEnumerable<TItem> itemsEnumerable) where TItem : T
    {
        var items = itemsEnumerable as IList<TItem> ?? itemsEnumerable?.ToList();

        if (items == null || items.Count == 0)
        {
            return;
        }

        HasRelevantItems = true;

        foreach (var fixableItem in items)
        {

            _itemsToFix.Add(fixableItem);
        }

    }

    public void Remove(T item)
    {
        _itemsToFix.Remove(item);
        HasRelevantItems = _itemsToFix.Count != 0;
    }

    public virtual void Fix()
    {
        for (var index = 0; index < _itemsToFix.Count; index++)
        {
            var item = _itemsToFix[index];

            if (item is IFixable fixableItem)
            {
                fixableItem.Fix();
            }

            _itemsToFix[index] = default;
        }

        Clear();
    }

    public void Clear()
    {
        _itemsToFix.Clear();
    }

    public abstract string ObjectType { get; }
}