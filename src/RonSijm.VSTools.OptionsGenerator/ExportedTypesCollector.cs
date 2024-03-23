using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Collections.Immutable;

namespace RonSijm.VSTools.OptionsGenerator;

internal class ExportedTypesCollector : SymbolVisitor
{
    private readonly HashSet<INamedTypeSymbol> _exportedTypes = new(SymbolEqualityComparer.Default);

    public ImmutableArray<INamedTypeSymbol> GetPublicTypes() => _exportedTypes.ToImmutableArray();

    public override void VisitAssembly(IAssemblySymbol symbol)
    {
        symbol.GlobalNamespace.Accept(this);
    }

    public override void VisitNamespace(INamespaceSymbol symbol)
    {
        foreach (var namespaceOrType in symbol.GetMembers())
        {
            namespaceOrType.Accept(this);
        }
    }

    public override void VisitNamedType(INamedTypeSymbol type)
    {

        if (!type.IsAccessibleOutsideOfAssembly() || !_exportedTypes.Add(type))
        {
            return;
        }

        var nestedTypes = type.GetTypeMembers();

        if (nestedTypes.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (var nestedType in nestedTypes)
        {
            nestedType.Accept(this);
        }
    }
}