using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

namespace RonSijm.VSTools.Module.NamespaceFixer.FolderFixing.Models;

public class ProjectToRenameFixer : IFixable, ILoggableItem
{
    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }


    public void Fix()
    {
        var casingIssue = CurrentItemValue.Equals(ExpectedItemValue, StringComparison.InvariantCultureIgnoreCase);

        // If there is a casing issue, first rename the item to "bak-" - because windows doesn't let you rename case-sensitive to the same filename.
        if (casingIssue)
        {
            File.Move(CurrentItemValue, $"{ExpectedItemValue}-vstoolBackup");
            File.Move($"{ExpectedItemValue}-vstoolBackup", ExpectedItemValue);
        }
        else
        {
            File.Move(CurrentItemValue, ExpectedItemValue);
        }

        if(File.Exists($"{CurrentItemValue}.DotSettings"))
        {
            File.Move($"{CurrentItemValue}.DotSettings", $"{ExpectedItemValue}.DotSettings");
        }
    }
}