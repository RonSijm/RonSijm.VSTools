namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Locators;

public class FixMemberAccessExpressionMismatchLocator : ISyntaxMismatchLocator
{
    public ushort Order => 2;

    public void Locate(SyntaxInFileToFixModel fileModel, NamespaceChangedCollectionModel namespaceCollection)
    {
        var memberAccessExpressionSyntaxes = fileModel.Root.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();

        foreach (var memberAccessExpressionSyntax in memberAccessExpressionSyntaxes)
        {
            var actualNamespace = memberAccessExpressionSyntax.ToString();

            var (namespaceReference, matchType) = namespaceCollection.Find(actualNamespace);

            if (namespaceReference == null)
            {
                continue;
            }

            var fullExpectedNamespace = actualNamespace.Replace(namespaceReference.OldNamespace, namespaceReference.ExpectedNamespace);
            var expectedNamespace = NamespaceHelper.FindNonCommonRootNamespace(fileModel.Namespace, fullExpectedNamespace);

            if (actualNamespace == expectedNamespace)
            {
                continue;
            }

            var itemToFix = new MemberAccessExpressionSyntaxFixer
            {
                ExpectedItemValue = expectedNamespace,
                CurrentItemValue = actualNamespace,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
                MatchType = matchType,
                Parent = fileModel,
            };

            fileModel.Add(itemToFix);
        }
    }
}