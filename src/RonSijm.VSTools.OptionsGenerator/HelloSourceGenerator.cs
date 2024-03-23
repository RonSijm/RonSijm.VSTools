using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

namespace RonSijm.VSTools.OptionsGenerator;

[Generator]
public class HelloSourceGenerator : ISourceGenerator
{
    private const string OptionsInterface = "RonSijm.VSTools.Core.DataContracts.IOptionsModel";
    private const string ProjectRootName = "RonSijm.VSTools";

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxTreeReceiver)
        {
            return;
        }

        var interfaceSymbol = FindInterfaceInReferencedProjects(context.Compilation, ProjectRootName);

        if (interfaceSymbol is not { TypeKind: TypeKind.Interface })
        {
            return;
        }

        var typesImplementingInterface = FindAllTypesThatImplementInterface(context, interfaceSymbol);

        var x = typesImplementingInterface;
    }

    private static List<INamedTypeSymbol> FindAllTypesThatImplementInterface(GeneratorExecutionContext context, INamedTypeSymbol interfaceSymbol)
    {
        // Find all types implementing the interface
        var typesImplementingInterface = new List<INamedTypeSymbol>();

        var source = GetReferences(context.Compilation, ProjectRootName);

        foreach (var assembly in source)
        {
            if (context.Compilation.GetAssemblyOrModuleSymbol(assembly) is not IAssemblySymbol referencedCompilation)
            {
                continue;
            }

            var visitor = new ExportedTypesCollector();
            visitor.Visit(referencedCompilation.GlobalNamespace);
            var result = visitor.GetPublicTypes();

            var options = result.Where(typeSymbol => typeSymbol != null && typeSymbol.AllInterfaces.Contains(interfaceSymbol));

            typesImplementingInterface.AddRange(options);
        }

        return typesImplementingInterface;
    }

    private static INamedTypeSymbol FindInterfaceInReferencedProjects(Compilation compilation, string inReferences)
    {
        INamedTypeSymbol interfaceSymbol = null;

        var source = GetReferences(compilation, inReferences);

        foreach (var reference in source)
        {
            var metadataReference = compilation.GetAssemblyOrModuleSymbol(reference) as IAssemblySymbol;

            interfaceSymbol = metadataReference?.GetTypeByMetadataName(OptionsInterface);

            if (interfaceSymbol != null)
            {
                break;
            }
        }

        return interfaceSymbol;
    }

    private static IEnumerable<MetadataReference> GetReferences(Compilation compilation, string inReferences)
    {
        var source = compilation.References;

        if (inReferences != null)
        {
            source = source.Where(x => x.Display != null && x.Display.Contains(inReferences));
        }

        return source;
    }

    // Add the source code to the compilation
    //context.AddSource($"{typeName}.g.cs", source);


    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new SyntaxTreeReceiver());
    }
}