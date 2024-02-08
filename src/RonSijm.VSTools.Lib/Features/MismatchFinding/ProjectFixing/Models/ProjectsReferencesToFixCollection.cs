namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ProjectsReferencesToFixCollection : FixableCollectionBase<ProjectsReferencesToFixModel>
{
    public override string ObjectType => "Projects Referencing Other Projects";
}