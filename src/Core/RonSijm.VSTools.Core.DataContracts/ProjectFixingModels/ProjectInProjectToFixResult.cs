using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

using ItemReference = RonSijm.VSTools.Core.DataContracts.FileModels.ItemReference;

namespace RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;

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