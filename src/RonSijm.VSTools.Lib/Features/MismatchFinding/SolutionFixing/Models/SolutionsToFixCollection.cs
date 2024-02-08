namespace RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;

public class SolutionsToFixCollection : INamedCollection, IHaveInnerReturnablesOfT<SolutionToFixModel>, IFixable
{
    public string ObjectType => "Solutions";

    public string ObjectName { get; set; }

    public void Fix()
    {
        foreach (var solutionToFixModel in InnerItems)
        {
            var solutionAsText = File.ReadAllText(solutionToFixModel.ObjectName);

            foreach (var project in solutionToFixModel.InnerItems)
            {
                solutionAsText = solutionAsText.Replace($"\"{project.CurrentItemValue}\"", $"\"{project.ExpectedItemValue}\"");
            }

            File.WriteAllText(solutionToFixModel.ObjectName, solutionAsText);
        }
    }

    public int RelevantItemCount => InnerItems.Count;
    public bool HasRelevantItems => RelevantItemCount != 0;

    public IReadOnlyList<SolutionToFixModel> InnerItems => _itemsToFix;
    private readonly List<SolutionToFixModel> _itemsToFix = [];

    public void Add(SolutionToFixModel item)
    {
        _itemsToFix.Add(item);
    }

    public void AddRange<TItem>(IEnumerable<TItem> items) where TItem : SolutionToFixModel
    {
        _itemsToFix.AddRange(items);
    }

    public void Remove(SolutionToFixModel item)
    {
        _itemsToFix.Remove(item);
    }
}