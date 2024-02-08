namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

internal class StringReplaceFixable : IValueFix, IMatchedNamespace, IHaveObjectType
{
    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }

    public NamespaceChangeMatchType MatchType { get; set; }

    public string ObjectType => "String Replace";
}