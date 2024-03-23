using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Helpers;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Fixers;

public class NamespaceDeclarationSyntaxFixer : IFixable, ILoggableItem, IHaveObjectType
{
    public string ObjectType => "Namespace Declaration";

    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }

    public SyntaxInFileToFixModel Parent { get; set; }

    public void Fix()
    {
        var currentNamespace = Parent.Root.DescendantNodes().GetNamespace();

        var newNamespaceDeclaration = currentNamespace.WithName(SyntaxFactory.ParseName(ExpectedItemValue));
        Parent.Root = Parent.Root.ReplaceNode(currentNamespace, newNamespaceDeclaration);
    }
}