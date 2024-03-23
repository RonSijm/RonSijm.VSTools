using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;

namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

public interface ICanHaveErrors
{
    IReadOnlyList<ErrorWhileMatchingResult> Errors { get; }
}