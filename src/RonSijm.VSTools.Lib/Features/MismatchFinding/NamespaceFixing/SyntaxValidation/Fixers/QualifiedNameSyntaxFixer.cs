namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public class QualifiedNameSyntaxFixer : BaseSyntaxFixer
{
    public override void Fix()
    {
        var qualifiedNameSyntaxes = Parent.Root.DescendantNodes().OfType<QualifiedNameSyntax>().Where(x => x.ToString() == CurrentItemValue).ToList();
        var qualifiedNameSyntax = qualifiedNameSyntaxes.FirstOrDefault();

        if (qualifiedNameSyntax != null)
        {
            var newName = ExpectedItemValue.ToQualifiedName().WithTrailingTrivia(SyntaxFactory.Space);
            Parent.Root = Parent.Root.ReplaceNode(qualifiedNameSyntax, newName);
        }
    }
}