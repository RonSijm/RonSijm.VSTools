namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public class MemberAccessExpressionSyntaxFixer : BaseSyntaxFixerOfT<MemberAccessExpressionSyntax>, IHaveObjectType
{
    public string ObjectType => "Member Access";

    protected override Func<MemberAccessExpressionSyntax, string> CurrentItemValueResolver => syntax => syntax.ToString();
}