using Microsoft.Build.Construction;
using RonSijm.VSTools.Lib.Features.ProjectFixing.Extensions;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Abstractions
{
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
}