using RonSijm.VSTools.Lib.Features.Core;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Abstractions;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ItemToFixResult : IFixable
{
    public ItemReference Reference { get; set; }
    public string ExpectedPath { get; set; }
    public string AbsoluteExpectedItemPath { get; set; }
    public string AbsoluteCurrentItemPath { get; set; }
    public string AbsoluteProjectPath { get; set; }

    public void Fix()
    {
        Reference.SetPath(ExpectedPath);
        Reference.SaveTarget.Save();
    }
}