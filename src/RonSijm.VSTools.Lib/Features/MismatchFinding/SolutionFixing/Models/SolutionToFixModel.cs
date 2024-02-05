namespace RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;

[DebuggerDisplay("{File}")]
public class SolutionToFixModel(string file) : ItemsToFixResponse
{
    public string File { get; } = file;
}