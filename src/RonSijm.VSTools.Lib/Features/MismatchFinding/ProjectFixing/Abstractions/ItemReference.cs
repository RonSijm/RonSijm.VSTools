namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Abstractions;

public partial class ItemReference
{
    public string Path { get; init; }
    public ISaveable SaveTarget { get; set; }

    public Action<string> SetPath { get; init; }
}