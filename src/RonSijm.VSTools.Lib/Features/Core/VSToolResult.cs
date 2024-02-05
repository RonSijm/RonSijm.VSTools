namespace RonSijm.VSTools.Lib.Features.Core;

public class VSToolResult
{
    public List<ProjectReferenceModel> InitResult { get; set; }
    public List<OneOf<ItemsToFixResponse, CollectionToFixResponse>> Results { get; set; } = [];

    public bool HasErrors
    {
        get
        {
            var hasSingularErrors = Results.Where(x => x.IsT0).Select(x => x.AsT0).Where(x => x.Errors != null).Any(x => x.Errors.Any());
            var hasCollectionErrors = Results.Where(x => x.IsT1).SelectMany(x => x.AsT1).Where(x => x.Errors != null).Any(x => x.Errors.Any());

            return hasSingularErrors || hasCollectionErrors;
        }
    }

    public bool HasItems
    {
        get
        {
            var hasSingularItems = Results.Where(x => x.IsT0).Select(x => x.AsT0).Any(x => x.HasItems);
            var hasCollectionItems = Results.Where(x => x.IsT1).SelectMany(x => x.AsT1).Any(x => x.HasItems);

            var hasInitResult = InitResult != null && InitResult.Any(x => !x.Existing);

            return hasSingularItems || hasCollectionItems;
        }
    }
}