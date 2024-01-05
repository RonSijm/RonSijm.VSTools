using RonSijm.VSTools.Lib.Features.ProjectFixing.Abstractions;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Models;

public class ProjectToFixResponse
{
    public List<(ItemReference reference, string expectedPath)> ProjectsToFix { get; set; } = new();
    public List<(ItemReference reference, string error)> Errors { get; set; } = new();
}