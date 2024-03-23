using RonSijm.VSTools.Core.DataContracts.FileModels;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;

namespace RonSijm.VSTools.Module.NamespaceFixer.SolutionFixing.Models;

[DebuggerDisplay("{ObjectName}")]
public class SolutionToFixModel(string file) : FixableCollectionBase<ProjectInProjectToFixResult>
{
    public override string ObjectType => "Solution";
    public override string ObjectName { get; set; } = file;
}