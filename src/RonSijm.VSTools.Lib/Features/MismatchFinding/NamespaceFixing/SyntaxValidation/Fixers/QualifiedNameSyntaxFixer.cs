namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public class QualifiedNameSyntaxFixer : BaseSyntaxFixer
{
    public override void Fix()
    {
        var qualifiedNameSyntax = Parent.Root.DescendantNodes().OfType<QualifiedNameSyntax>().FirstOrDefault(x => x.ToString() == CurrentItemValue);

        if (qualifiedNameSyntax != null)
        {
            var newName = ExpectedItemValue.ToQualifiedName();
            Parent.Root = Parent.Root.ReplaceNode(qualifiedNameSyntax, newName);
        }
    }
}