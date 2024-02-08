namespace RonSijm.VSTools.Lib.Features.Core.Interfaces.CollectionInterfaces;

public interface IHaveInnerReturnables : IReturnable
{
    IReadOnlyList<object> InnerItems { get; }

    public int RelevantItemCount => this.GetCount();
}