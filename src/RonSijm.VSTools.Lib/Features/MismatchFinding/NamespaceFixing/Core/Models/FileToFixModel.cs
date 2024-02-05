namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.Core.Models;

public class FileToFixModel : FileModel, IFixableItem
{
    public SyntaxTree SyntaxTree { get; set; }
    public CompilationUnitSyntax Root { get; set; }
    public List<SyntaxNode> DescendantNodes { get; set; }

    public List<IFixableItem> ItemsToFix { get; set; } = [];
    public void Fix()
    {
        foreach (var fixableItem in ItemsToFix)
        {
            fixableItem.Fix();
        }

        var newFileContent = Root.NormalizeWhitespace(elasticTrivia: true).ToFullString();
        File.WriteAllText(FileName, newFileContent);
    }

    public string CurrentItemDisplayValue => FileName;

    // ReSharper disable once UnassignedGetOnlyAutoProperty - Unused in this class.
    public string ExpectedItemValue { get; }
    public string ExpectedItemDisplayValue => $"Items to fix: {ItemsToFix.Count}";
    public string CurrentItemValue { get; set; }
}