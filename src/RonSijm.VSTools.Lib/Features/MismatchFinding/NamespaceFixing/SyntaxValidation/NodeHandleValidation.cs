namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation;

public static class NodeHandleValidation
{
    public static NodeHandledType NodeHandled(QualifiedNameSyntax node)
    {
        // Already handled by different verification.
        if (node.Parent is UsingDirectiveSyntax)
        {
            return NodeHandledType.Handled;
        }

        if (node.Parent is FileScopedNamespaceDeclarationSyntax)
        {
            return NodeHandledType.Handled;
        }

        if (node.Parent is NamespaceDeclarationSyntax)
        {
            return NodeHandledType.Handled;
        }

        if (node.Parent is QualifiedNameSyntax)
        {
            return NodeHandledType.Handled;
        }

        return NodeHandledType.Unhandled;
    }
}