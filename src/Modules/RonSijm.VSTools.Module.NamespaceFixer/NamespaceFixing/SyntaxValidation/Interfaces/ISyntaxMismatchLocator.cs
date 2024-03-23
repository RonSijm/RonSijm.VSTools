using RonSijm.VSTools.Core.DataContracts.NamespaceModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Interfaces;

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