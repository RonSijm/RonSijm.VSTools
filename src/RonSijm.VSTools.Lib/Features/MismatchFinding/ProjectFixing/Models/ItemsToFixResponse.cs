namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ItemsToFixResponse
{
    public bool HasItems { get; private set; }

    public IReadOnlyList<ItemToFixResult> ItemsToFix => _itemsToFix;
    private readonly List<ItemToFixResult> _itemsToFix = [];

    public IReadOnlyList<ErrorWhileMatchingResult> Errors => _errors;
    private readonly List<ErrorWhileMatchingResult> _errors = [];


    public void Add(ErrorWhileMatchingResult error)
    {
        HasItems = true;
        _errors.Add(error);
    }

    public void Add(ItemToFixResult error)
    {
        HasItems = true;
        _itemsToFix.Add(error);
    }
}