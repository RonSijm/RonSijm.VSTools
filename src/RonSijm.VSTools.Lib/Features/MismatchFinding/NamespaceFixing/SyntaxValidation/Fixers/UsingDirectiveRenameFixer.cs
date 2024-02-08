namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

[DebuggerDisplay("[{GetType().Name}]-[{MatchType}]: {CurrentItemDisplayValue} => {ExpectedItemDisplayValue}")]
public class UsingDirectiveRenameFixer : IFixable, IMatchedNamespace, ILoggableItem, IHaveObjectType
{
    public string ObjectType => "Using Directive";

    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }
    public SyntaxInFileToFixModel Parent { get; set; }
    public NamespaceChangeMatchType MatchType { get; set; }

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