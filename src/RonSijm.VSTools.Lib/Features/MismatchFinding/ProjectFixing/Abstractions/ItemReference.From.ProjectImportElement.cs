using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Extensions;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Abstractions;

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