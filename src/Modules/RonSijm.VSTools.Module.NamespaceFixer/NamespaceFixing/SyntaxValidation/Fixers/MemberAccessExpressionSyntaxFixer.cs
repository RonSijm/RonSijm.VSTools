using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Fixers;

public class MemberAccessExpressionSyntaxFixer : BaseSyntaxFixerOfT<MemberAccessExpressionSyntax>, IHaveObjectType
{
    public string ObjectType => "Member Access";

    protected override Func<MemberAccessExpressionSyntax, string> CurrentItemValueResolver => syntax => syntax.ToString();
}