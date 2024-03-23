using ItemReference = RonSijm.VSTools.Core.DataContracts.FileModels.ItemReference;

namespace RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;

public class ErrorWhileMatchingResult
{
    public ItemReference Reference { get; set; }
    public string Error { get; set; }
    public List<string> ErrorReferences { get; set; }
}