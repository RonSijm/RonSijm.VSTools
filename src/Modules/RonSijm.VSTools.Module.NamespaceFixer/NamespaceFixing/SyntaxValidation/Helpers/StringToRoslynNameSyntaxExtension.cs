namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Helpers;

public static class StringToRoslynNameSyntaxExtension
{
    public static NameSyntax ToQualifiedName(this string fullNamespaceName)
    {
        var typeNameParts = fullNamespaceName.Split('.');

        NameSyntax nameSyntax = SyntaxFactory.IdentifierName(typeNameParts[0]);

        for (var i = 1; i < typeNameParts.Length; i++)
        {
            nameSyntax = SyntaxFactory.QualifiedName(nameSyntax, SyntaxFactory.IdentifierName(typeNameParts[i]));
        }

        return nameSyntax;
    }
}