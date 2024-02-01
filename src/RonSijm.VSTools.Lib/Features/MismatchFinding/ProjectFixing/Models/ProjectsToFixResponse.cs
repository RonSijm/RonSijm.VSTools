using RonSijm.VSTools.Lib.Features.Core;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ProjectsToFixResponse : ItemsToFixResponse, IFixable
{
    public void Fix()
    {
        foreach (var item in ItemsToFix)
        {
            item.Fix();
        }
    }
}