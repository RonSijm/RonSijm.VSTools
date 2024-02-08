namespace RonSijm.VSTools.Lib.Features.Core.Models;

public partial class ItemReference
{
    public string Path { get; init; }
    public ISaveable SaveTarget { get; set; }

    public Action<string> SetPath { get; init; }
}