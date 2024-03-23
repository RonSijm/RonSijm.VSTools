using RonSijm.VSTools.Core.DataContracts.Helpers;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;

public interface IHaveInnerReturnables : IReturnable
{
    IReadOnlyList<object> InnerItems { get; }

    public int RelevantItemCount => this.GetCount();
}