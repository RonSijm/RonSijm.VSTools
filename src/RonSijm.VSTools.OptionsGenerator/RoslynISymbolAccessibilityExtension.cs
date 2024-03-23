using Microsoft.CodeAnalysis;

namespace RonSijm.VSTools.OptionsGenerator;

public static class RoslynISymbolAccessibilityExtension
{
    public static bool IsAccessibleOutsideOfAssembly(this ISymbol symbol)
    {
        return symbol.DeclaredAccessibility switch
        {
            Accessibility.Private => false,
            Accessibility.Internal => false,
            Accessibility.ProtectedAndInternal => false,
            Accessibility.Protected => true,
            Accessibility.ProtectedOrInternal => true,
            Accessibility.Public => true,
            _ => true, //Here should be some reasonable default
        };
    }
}