namespace RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;

[DebuggerDisplay("{ObjectName}")]
public class SolutionToFixModel(string file) : FixableCollectionBase<ProjectInProjectToFixResult>
{
    public override string ObjectType => "Solution";
    public override string ObjectName { get; set; } = file;
}