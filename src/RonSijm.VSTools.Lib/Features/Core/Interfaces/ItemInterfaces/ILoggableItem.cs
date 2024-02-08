namespace RonSijm.VSTools.Lib.Features.Core.Interfaces.ItemInterfaces;

public interface ILoggableItem : IValueUpdateItem
{
    string CurrentItemDisplayValue { get; }
    string ExpectedItemDisplayValue { get; }
}