namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;

public class NamespaceChangedCollectionModel
{
    protected List<NamespaceReferenceModel> Values { get; set; } = [];
    protected Dictionary<string, NamespaceReferenceModel> DictValues { get; set; }

    protected Dictionary<string, (NamespaceReferenceModel namespaceReference, NamespaceChangeMatchType matchType)> ValueCache { get; set; } = [];

    public void AddRange(List<NamespaceReferenceModel> result)
    {
        Values.AddRange(result);
    }

    public void AddRange(NamespaceChangedCollectionModel renamedNamespaces)
    {
        Values.AddRange(renamedNamespaces.Values);
    }

    public NamespaceChangedCollectionModel Rebuild()
    {
        var values = Values.DistinctBy(x => x.OldNamespace).OrderByDescending(x => x.OldNamespace.Length).ToList();
        DictValues = values.ToDictionary(x => x.OldNamespace);
        ValueCache = [];

        return new NamespaceChangedCollectionModel { Values = values };
    }

    public (NamespaceReferenceModel namespaceReference, NamespaceChangeMatchType matchType) Find(string nameSpace)
    {
        var isCacheResult = ValueCache.TryGetValue(nameSpace, out var isCached);
        if (isCacheResult)
        {
            return isCached;
        }

        BreakOnFileHelper.BreakOnNamespaceLookup(nameSpace);

        var isRenamedResult = DictValues.TryGetValue(nameSpace, out var isRenamed);

        if (isRenamedResult)
        {
            var result = (isRenamed, NamespaceChangeMatchType.Exact);
            ValueCache.Add(nameSpace, result);
            return result;
        }

        var noStartWithresult = ((NamespaceReferenceModel)null, NamespaceChangeMatchType.NoMatch);
        ValueCache.Add(nameSpace, noStartWithresult);
        return noStartWithresult;

#pragma warning disable CS0162 // Unreachable code detected
        // ReSharper disable HeuristicUnreachableCode - Justification: Ideally we can fix everything by exact match.
        // If there are no exact matches, we should add extra inspectors to find that correct old namespace, instead of trying to find it with less precision

        var isRenamedByStartsWith = Values.FirstOrDefault(x => nameSpace.StartsWith(x.OldNamespace, StringComparison.OrdinalIgnoreCase));

        if (isRenamedByStartsWith == null)
        {
            var result = ((NamespaceReferenceModel)null, NamespaceChangeMatchType.NoMatch);
            ValueCache.Add(nameSpace, result);
            return result;
        }

        if (isRenamedByStartsWith.OldNamespace == isRenamedByStartsWith.ExpectedNamespace)
        {
            var result = ((NamespaceReferenceModel)null, NamespaceChangeMatchType.AlreadyMatches);
            ValueCache.Add(nameSpace, result);
            return result;
        }
        else
        {
            var result = (isRenamedByStartsWith, NamespaceChangeMatchType.StartsWith);
            ValueCache.Add(nameSpace, result);
            return result;
        }
#pragma warning restore CS0162 // Unreachable code detected
    }

    public void Add(NamespaceReferenceModel item)
    {
        Values.Add(item);
    }
}