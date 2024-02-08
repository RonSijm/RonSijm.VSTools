using System.Text.RegularExpressions;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Locators;

public class RazorSyntaxMismatchLocator : ISyntaxMismatchLocator
{
    public ushort Order => 5;

    public bool CanHandle(SyntaxInFileToFixModel fileModel)
    {
        return fileModel.FileModel.FileName.EndsWith(".razor", StringComparison.InvariantCultureIgnoreCase);
    }

    public void Locate(SyntaxInFileToFixModel fileModel, NamespaceChangedCollectionModel namespaceCollection)
    {
        var text = File.ReadAllText(fileModel.FileModel.FileName);
        var usingMatches = Regex.Matches(text, @"@using\s+(.*)");

        foreach (Match usingMatch in usingMatches)
        {
            var currentItemValue = usingMatch.Value.Replace("\r", string.Empty).Replace("\n", string.Empty);
            var actualNamespace = currentItemValue.Split(' ').Last();

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

            var replaceNamespace = currentItemValue.Replace(namespaceReference.OldNamespace, namespaceReference.ExpectedNamespace);

            var itemToFix = new StringReplaceFixable
            {
                ExpectedItemValue = replaceNamespace,
                CurrentItemValue = currentItemValue,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
                MatchType = matchType
            };

            fileModel.Add(itemToFix);
        }
    }
}