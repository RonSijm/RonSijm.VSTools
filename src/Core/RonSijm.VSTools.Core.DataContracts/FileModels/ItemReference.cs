using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels.Abstractions;

namespace RonSijm.VSTools.Core.DataContracts.FileModels;

public partial class ItemReference
{
    public string Path { get; init; }
    public ISaveable SaveTarget { get; set; }

    public Action<string> SetPath { get; init; }
}