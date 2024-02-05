namespace RonSijm.VSTools.Lib.Features.MismatchFinding.FolderFixing.Models;

public class ProjectToRenameFixer : IFixableItem
{
    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }


    public void Fix()
    {
        File.Move(CurrentItemValue, ExpectedItemValue);

        if(File.Exists($"{CurrentItemValue}.DotSettings"))
        {
            File.Move($"{CurrentItemValue}.DotSettings", $"{ExpectedItemValue}.DotSettings");
        }
    }
}