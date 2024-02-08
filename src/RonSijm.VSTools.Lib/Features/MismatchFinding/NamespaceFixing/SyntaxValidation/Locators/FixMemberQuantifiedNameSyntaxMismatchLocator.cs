namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Locators;

public class FixMemberQuantifiedNameSyntaxMismatchLocator : ISyntaxMismatchLocator
{
    public ushort Order => 3;

    public void Locate(SyntaxInFileToFixModel fileModel, NamespaceChangedCollectionModel namespaceCollection)
    {
        var qualifiedNameSyntaxes = fileModel.Root.DescendantNodes().OfType<QualifiedNameSyntax>().ToList();

        foreach (var qualifiedNameSyntax in qualifiedNameSyntaxes)
        {
            var noteHandled = NodeHandleValidation.NodeHandled(qualifiedNameSyntax);

            if (noteHandled == NodeHandledType.Handled)
            {
                continue;
            }

            var actualNamespace = qualifiedNameSyntax.ToString();

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

            var itemToFix = new QualifiedNameSyntaxFixer
            {
                ExpectedItemValue = expectedNamespace,
                CurrentItemValue = actualNamespace,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
                MatchType = matchType,
                NoteHandledType = noteHandled,
                Parent = fileModel,
            };

            fileModel.Add(itemToFix);
        }
    }
}