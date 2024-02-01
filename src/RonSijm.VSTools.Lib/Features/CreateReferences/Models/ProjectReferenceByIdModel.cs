namespace RonSijm.VSTools.Lib.Features.CreateReferences.Models;

public class ProjectReferenceByIdModel
{
    public string ProjectReferenceId { get; set; }
    public List<string> ProjectNames { get; set; } = new();
}