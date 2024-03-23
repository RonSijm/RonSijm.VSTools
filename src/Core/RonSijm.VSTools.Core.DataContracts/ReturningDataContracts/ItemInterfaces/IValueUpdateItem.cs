namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

public interface IValueUpdateItem
{
    string ExpectedItemValue { get; }
    string CurrentItemValue { get; }
}