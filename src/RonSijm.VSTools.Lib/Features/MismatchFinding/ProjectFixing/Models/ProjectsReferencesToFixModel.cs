namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ProjectsReferencesToFixModel : FixableCollectionBase<ProjectInProjectToFixResult>, INamedCollection
{
    public override string ObjectType => "Project Reference In Project";
}