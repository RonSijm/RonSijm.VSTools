namespace RonSijm.VSTools.Lib.Features.Core;

public class VSToolResult : IHaveInnerReturnablesOfT<INamedCollection>
{
    public List<ProjectReferenceModel> InitResult { get; set; }

    public bool HasErrors => false;

    public IReadOnlyList<INamedCollection> InnerItems { get; set; } = [];
}