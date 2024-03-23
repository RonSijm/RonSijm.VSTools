namespace RonSijm.VSTools.Core.DataContracts.CoreModels;

public class ProjectReferenceModel : ProjectLoadedModel
{
    public bool Existing { get; set; }
    public string ProjectReferenceId { get; set; }
}