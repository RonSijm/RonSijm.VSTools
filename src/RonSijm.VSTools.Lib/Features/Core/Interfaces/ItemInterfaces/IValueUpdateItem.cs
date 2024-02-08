namespace RonSijm.VSTools.Lib.Features.Core.Interfaces.ItemInterfaces;

public interface IValueUpdateItem
{
    string ExpectedItemValue { get; }
    string CurrentItemValue { get; }
}