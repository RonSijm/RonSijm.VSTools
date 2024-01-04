using Microsoft.Build.Construction;

namespace RonSijm.VSTools.Lib.Features.ProjectFixing.Models;

public class ProjectToFixResponse
{
    public List<(ProjectItemElement reference, string expectedPath)> ProjectsToFix { get; set; } = new();
    public List<(ProjectItemElement reference, string error)> Errors { get; set; } = new();
}