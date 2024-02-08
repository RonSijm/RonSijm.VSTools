namespace RonSijm.VSTools.Lib.Features.CreateReferences.Models;

public class ProjectReferenceModel : ProjectLoadedModel
{
    public bool Existing { get; set; }
    public string ProjectReferenceId { get; set; }
}