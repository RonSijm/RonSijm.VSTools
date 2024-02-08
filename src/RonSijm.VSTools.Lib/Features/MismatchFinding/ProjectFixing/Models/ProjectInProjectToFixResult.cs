namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ProjectInProjectToFixResult : IFixable, ILoggableItem, IHaveName
{
    public ItemReference Reference { get; set; }

    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }

    public void Fix()
    {
        Reference.SetPath(ExpectedItemValue);
        Reference.SaveTarget.Save();

        Reference = null;
    }

    public string ObjectName => Reference.Path;
}