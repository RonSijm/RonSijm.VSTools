using RonSijm.VSTools.Core.DataContracts.FileModels;

namespace RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;

public class ProjectsReferencesToFixCollection : FixableCollectionBase<ProjectsReferencesToFixModel>
{
    public override string ObjectType => "Projects Referencing Other Projects";
}