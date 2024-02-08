namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Interfaces;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface ISyntaxMismatchLocator
{
    public ushort Order { get; }

    bool CanHandle(SyntaxInFileToFixModel fileModel)
    {
        return true;
    }

    void Locate(SyntaxInFileToFixModel fileModel, NamespaceChangedCollectionModel namespaceCollection);
}