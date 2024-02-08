namespace RonSijm.VSTools.Lib.Features.Core.Models;

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