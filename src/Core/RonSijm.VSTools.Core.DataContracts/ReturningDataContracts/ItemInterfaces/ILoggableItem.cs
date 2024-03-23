namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

public interface ILoggableItem : IValueUpdateItem
{
    string CurrentItemDisplayValue { get; }
    string ExpectedItemDisplayValue { get; }
}