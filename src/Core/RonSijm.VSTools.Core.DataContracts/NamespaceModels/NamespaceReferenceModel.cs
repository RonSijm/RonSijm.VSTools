using RonSijm.VSTools.Core.DataContracts.Helpers;

namespace RonSijm.VSTools.Core.DataContracts.NamespaceModels;

[DebuggerDisplay("[{GetType().Name}]-[{ChangeType}]: {OldNamespace} => {ExpectedNamespace}")]
public class NamespaceReferenceModel
{
    private string _expectedNamespace;
    private string _oldNamespace;

    public NamespaceReferenceModel(string oldNamespace, string expectedNamespace, NamespaceChangeType changeType)
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