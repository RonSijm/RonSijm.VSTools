namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public class IdentifierNameSyntaxFixer : BaseSyntaxFixerOfT<MemberAccessExpressionSyntax>
{
    protected override Func<MemberAccessExpressionSyntax, string> CurrentItemValueResolver => syntax => syntax.ToString();
}