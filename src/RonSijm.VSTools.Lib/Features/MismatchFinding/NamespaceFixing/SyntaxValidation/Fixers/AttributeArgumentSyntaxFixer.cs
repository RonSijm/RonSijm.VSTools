namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public class AttributeArgumentSyntaxFixer : BaseSyntaxFixer
{
    public override void Fix()
    {
        var attributeNode = Parent.Root.DescendantNodes().OfType<AttributeArgumentSyntax>().FirstOrDefault(x => x.ToString() == CurrentItemValue);

        if (attributeNode != null)
        {
            var child = attributeNode.ChildNodes().OfType<LiteralExpressionSyntax>().FirstOrDefault(x => x.ToString() == CurrentItemValue);

            if (child != null)
            {
                Parent.Root = Parent.Root.ReplaceNode(child, SyntaxFactory.ParseExpression(ExpectedItemValue));
            }
        }
    }
}