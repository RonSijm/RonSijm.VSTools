namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation.Fixers;

internal class StringReplaceFixable : IFileManipulationItem
{
    public void Fix()
    {
    }

    public string CurrentItemDisplayValue { get; set; }
    public string ExpectedItemValue { get; set; }
    public string ExpectedItemDisplayValue { get; set; }
    public string CurrentItemValue { get; set; }
}