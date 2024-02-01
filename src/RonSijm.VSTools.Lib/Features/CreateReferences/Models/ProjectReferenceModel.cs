namespace RonSijm.VSTools.Lib.Features.CreateReferences.Models;

public class ProjectReferenceModel
{
    public bool Existing { get; set; }
    public string ProjectReferenceId { get; set; }

    public ProjectRootElement Project { get; set; }
}