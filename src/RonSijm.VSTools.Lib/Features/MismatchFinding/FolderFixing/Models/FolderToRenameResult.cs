namespace RonSijm.VSTools.Lib.Features.MismatchFinding.FolderFixing.Models;

public class FolderToRenameResult : IFixable
{
    public string CurrentItemValue { get; set; }
    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }

    public void Fix()
    {
        var casingIssue = CurrentItemValue.Equals(ExpectedItemValue, StringComparison.InvariantCultureIgnoreCase);

        // If there is a casing issue, first rename the item to "bak-" - because windows doesn't let you rename case-sensitive to the same filename.
        if (casingIssue)
        {
            Directory.Move(CurrentItemValue, $"{ExpectedItemValue}-vstoolBackup");
            Directory.Move($"{ExpectedItemValue}-vstoolBackup", ExpectedItemValue);
        }
        else
        {
            Directory.Move(CurrentItemValue, ExpectedItemValue);
        }
    }
}