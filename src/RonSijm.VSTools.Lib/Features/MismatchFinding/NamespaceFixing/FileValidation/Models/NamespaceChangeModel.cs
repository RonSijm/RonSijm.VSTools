using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Helpers;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;

[DebuggerDisplay("[{ChangeType}]: {OldNamespace} => {ExpectedNamespace}")]
public class NamespaceChangeModel
{
    private string _expectedNamespace;
    private string _oldNamespace;

    public NamespaceChangeModel()
    {
    }

    public NamespaceChangeModel(string oldNamespace, string expectedNamespace, NamespaceChangeType changeType)
    {
        OldNamespace = oldNamespace;
        ExpectedNamespace = expectedNamespace;
        ChangeType = changeType;
    }

    public string OldNamespace
    {
        get => _oldNamespace;
        set => _oldNamespace = value.RemoveInvalidNamespaceCharacters();
    }

    public string ExpectedNamespace
    {
        get => _expectedNamespace;
        set => _expectedNamespace = value.RemoveInvalidNamespaceCharacters();
    }

    public NamespaceChangeType ChangeType { get; set; }
}