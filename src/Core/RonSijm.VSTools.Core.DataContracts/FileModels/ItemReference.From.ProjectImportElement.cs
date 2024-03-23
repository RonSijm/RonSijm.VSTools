using RonSijm.VSTools.Core.DataContracts.Helpers;

namespace RonSijm.VSTools.Core.DataContracts.FileModels;

public partial class ItemReference
{
    public static implicit operator ItemReference(ProjectImportElement projectImport)
    {
        return new ItemReference
        {
            Path = projectImport.Project,
            SetPath = path => projectImport.Project = path,
            SaveTarget = projectImport.Parent.GetRoot()
        };
    }
}