namespace RonSijm.VSTools.Lib.Features.MismatchFinding.FolderFixing.Models;

public class FolderToRenameResult : IFixableItem
{
    public string CurrentItemValue { get; set; }
    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }

    public void Fix()
    {
        Directory.Move(CurrentItemValue, ExpectedItemValue);
    }
}