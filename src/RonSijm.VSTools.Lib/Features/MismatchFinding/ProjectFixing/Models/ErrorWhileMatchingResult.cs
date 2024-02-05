namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ErrorWhileMatchingResult
{
    public ItemReference Reference { get; set; }
    public string Error { get; set; }
    public List<string> ErrorReferences { get; set; }
}