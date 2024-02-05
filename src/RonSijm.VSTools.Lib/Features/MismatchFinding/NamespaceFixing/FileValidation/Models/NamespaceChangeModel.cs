using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Helpers;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;

public class NamespaceChangeModel
{
    private string _expectedNamespace;

    public NamespaceChangeModel()
    {
    }

    public NamespaceChangeModel(string oldNamespace, string expectedNamespace)
    {
        OldNamespace = oldNamespace;
        ExpectedNamespace = expectedNamespace;
    }

    public string OldNamespace { get; set; }

    public string ExpectedNamespace
    {
        get => _expectedNamespace;
        set => _expectedNamespace = value.ToNamespace();
    }

    public static implicit operator NamespaceChangeModel((string oldNamespace, string expectedNamespace) changes)
    {
        return new NamespaceChangeModel(changes.oldNamespace, changes.expectedNamespace);
    }
}