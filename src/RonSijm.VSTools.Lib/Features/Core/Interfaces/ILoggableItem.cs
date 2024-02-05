namespace RonSijm.VSTools.Lib.Features.Core.Interfaces;

public interface ILoggableItem
{
    string CurrentItemDisplayValue { get; }
    string ExpectedItemValue { get; }
    string ExpectedItemDisplayValue { get; }
    string CurrentItemValue { get; }
}