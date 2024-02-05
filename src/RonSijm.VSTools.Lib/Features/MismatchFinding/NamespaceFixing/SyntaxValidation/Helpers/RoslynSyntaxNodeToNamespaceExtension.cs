namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Helpers;

public static class RoslynSyntaxNodeToNamespaceExtension
{
    public static BaseNamespaceDeclarationSyntax GetNamespace(this IEnumerable<SyntaxNode> nodes)
    {
        var namespaceDeclarationSyntax = nodes.OfType<BaseNamespaceDeclarationSyntax>().FirstOrDefault();

        return namespaceDeclarationSyntax;
    }
}