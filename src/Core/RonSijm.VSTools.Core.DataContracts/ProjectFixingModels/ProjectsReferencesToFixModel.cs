using RonSijm.VSTools.Core.DataContracts.FileModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;

namespace RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;

public class ProjectsReferencesToFixModel : FixableCollectionBase<ProjectInProjectToFixResult>, INamedCollection
{
    public override string ObjectType => "Project Reference In Project";
}