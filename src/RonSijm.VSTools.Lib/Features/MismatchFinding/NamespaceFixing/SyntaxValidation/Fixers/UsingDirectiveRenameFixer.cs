namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

public class UsingDirectiveRenameFixer : IFixableItem
{
    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }
    public FileToFixModel Parent { get; set; }

    public void Fix()
    {
        var newUsingDirectives = Parent.Root.Usings.Select(usingDirective =>
            usingDirective.NormalizeWhitespace().NamespaceOrType.ToString() == CurrentItemValue
                ? usingDirective.WithName(SyntaxFactory.ParseName(ExpectedItemValue))
                : usingDirective
        );

        Parent.Root = Parent.Root.WithUsings(SyntaxFactory.List(newUsingDirectives));
    }
}