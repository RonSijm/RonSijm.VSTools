namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public abstract class BaseSyntaxFixerOfT<T> : BaseSyntaxFixer where T : SyntaxNode
{
    protected abstract Func<T, string> CurrentItemValueResolver { get; }

    public override void Fix()
    {
        var currentDeclaration = Parent.Root.DescendantNodes().OfType<T>().FirstOrDefault(x =>
        {
            var value = CurrentItemValueResolver(x);
            return value == CurrentItemValue;
        });

        if (currentDeclaration != null)
        {
            Parent.Root = Parent.Root.ReplaceNode(currentDeclaration, SyntaxFactory.ParseExpression(ExpectedItemValue));
        }
    }
}