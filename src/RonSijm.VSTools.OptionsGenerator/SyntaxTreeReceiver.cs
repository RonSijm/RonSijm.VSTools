using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RonSijm.VSTools.OptionsGenerator;

public class SyntaxTreeReceiver : ISyntaxReceiver
{
    public List<SyntaxTree> CandidateSyntaxTrees { get; } = new List<SyntaxTree>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax)
        {
            // Add the syntax tree containing the type declaration to the candidate syntax trees
            CandidateSyntaxTrees.Add(typeDeclarationSyntax.SyntaxTree);
        }
    }
}