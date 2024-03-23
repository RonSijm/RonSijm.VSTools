using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;

namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts;

public class VSToolResult : IHaveInnerReturnablesOfT<INamedCollection>
{
    public List<ProjectReferenceModel> InitResult { get; set; }

    public bool HasErrors => false;

    public IReadOnlyList<INamedCollection> InnerItems { get; set; } = new List<INamedCollection>();
}