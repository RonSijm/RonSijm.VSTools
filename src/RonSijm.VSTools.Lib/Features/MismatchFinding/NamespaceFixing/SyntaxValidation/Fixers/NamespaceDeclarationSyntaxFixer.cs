namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public class NamespaceDeclarationSyntaxFixer : IFixable, ILoggableItem, IHaveObjectType
{
    public string ObjectType => "Namespace Declaration";

    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }

    public SyntaxInFileToFixModel Parent { get; set; }

    public void Fix()
    {
        var currentNamespace = Parent.Root.DescendantNodes().GetNamespace();

        var newNamespaceDeclaration = currentNamespace.WithName(SyntaxFactory.ParseName(ExpectedItemValue));
        Parent.Root = Parent.Root.ReplaceNode(currentNamespace, newNamespaceDeclaration);
    }
}