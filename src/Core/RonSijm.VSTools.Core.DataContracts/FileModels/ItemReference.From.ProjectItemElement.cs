using RonSijm.VSTools.Core.DataContracts.Helpers;

namespace RonSijm.VSTools.Core.DataContracts.FileModels;

public partial class ItemReference
{
    public static implicit operator ItemReference(ProjectItemElement projectItemElement)
    {
        return new ItemReference
        {
            Path = projectItemElement.Include,
            SetPath = path => projectItemElement.Include = path,
            SaveTarget = projectItemElement.GetRoot()
        };
    }
}