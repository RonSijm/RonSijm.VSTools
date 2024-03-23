using RonSijm.VSTools.Core.DataContracts.NamespaceModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Fixers;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Interfaces;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Locators;

public class FixAttributeSyntaxMismatchLocator : ISyntaxMismatchLocator
{
    public ushort Order => 4;

    public void Locate(SyntaxInFileToFixModel fileModel, NamespaceChangedCollectionModel namespaceCollection)
    {
        var literalExpressions = fileModel.DescendantNodes.OfType<AttributeArgumentSyntax>().ToList();

        foreach (var literalExpressionSyntax in literalExpressions)
        {
            var literalValueQuoted = literalExpressionSyntax.ToString();

            if (!literalValueQuoted.StartsWith('"'))
            {
                continue;
            }

            var literalValueUnquoted = literalValueQuoted.Substring(1, literalValueQuoted.Length - 2);

            var (namespaceReference, matchType) = namespaceCollection.Find(literalValueUnquoted);

            if (namespaceReference == null)
            {
                continue;
            }

            var expectedNamespace = literalValueUnquoted.Replace(namespaceReference.OldNamespace, namespaceReference.ExpectedNamespace);

            if (literalValueUnquoted == expectedNamespace)
            {
                continue;
            }

            var itemToFix = new AttributeArgumentSyntaxFixer
            {
                ExpectedItemValue = $"\"{expectedNamespace}\"",
                CurrentItemValue = literalValueQuoted,
                ExpectedItemDisplayValue = $"\"{expectedNamespace}\"",
                CurrentItemDisplayValue = literalValueQuoted,
                Parent = fileModel,
                MatchType = matchType,
            };

            fileModel.Add(itemToFix);
        }
    }
}