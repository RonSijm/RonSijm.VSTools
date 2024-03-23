namespace RonSijm.VSTools.Core.DataContracts;

public class ProjectReferenceByIdModel
{
    public string ProjectReferenceId { get; set; }
    public List<string> ProjectNames { get; set; } = [];
}