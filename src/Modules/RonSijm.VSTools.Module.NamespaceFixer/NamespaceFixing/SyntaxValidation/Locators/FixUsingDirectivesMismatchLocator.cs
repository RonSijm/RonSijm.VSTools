using RonSijm.VSTools.Core.DataContracts.NamespaceModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Fixers;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Interfaces;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Locators;

public class FixUsingDirectivesMismatchLocator : ISyntaxMismatchLocator
{
    public ushort Order => 1;

    public void Locate(SyntaxInFileToFixModel fileModel, NamespaceChangedCollectionModel namespaceCollection)
    {
        var usingDeclarations = fileModel.DescendantNodes.OfType<UsingDirectiveSyntax>().ToList();

        foreach (var namespaceDeclaration in usingDeclarations)
        {
            var actualNamespace = namespaceDeclaration.NormalizeWhitespace().NamespaceOrType.ToString();

            var (namespaceReference, matchType) = namespaceCollection.Find(actualNamespace);

            if (namespaceReference == null)
            {
                continue;
            }

            var expectedNamespace = actualNamespace.Replace(namespaceReference.OldNamespace, namespaceReference.ExpectedNamespace);

            if (actualNamespace == expectedNamespace)
            {
                continue;
            }

            var itemToFix = new UsingDirectiveRenameFixer
            {
                ExpectedItemValue = expectedNamespace,
                CurrentItemValue = actualNamespace,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
                Parent = fileModel,
                MatchType = matchType
            };

            fileModel.Add(itemToFix);
        }
    }
}