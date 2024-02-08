namespace RonSijm.VSTools.Lib.Features.Core.Interfaces.ItemInterfaces;

public interface ICanHaveErrors
{
    IReadOnlyList<ErrorWhileMatchingResult> Errors { get; }
}