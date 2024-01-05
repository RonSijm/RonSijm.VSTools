using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Abstractions
{
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
}