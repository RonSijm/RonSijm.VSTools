namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.Core.Models;

public class FileToFixModel : FileModel, IFixableItem
{
    public SyntaxTree SyntaxTree { get; set; }
    public CompilationUnitSyntax Root { get; set; }
    public List<SyntaxNode> DescendantNodes { get; set; }

    public List<IFixableItem> ItemsToFix { get; set; } = [];
    public void Fix()
    {
        if (ItemsToFix.Count == 0)
        {
            return;
        }

        BreakOnFileHelper.BreakOnFile(FileName);

        foreach (var fixableItem in ItemsToFix.Where(x => x is not IFileManipulationItem))
        {
            fixableItem.Fix();
        }

        var newFileContent = FileExportFormatExtension.SyntaxNodeToString(this);

        foreach (var fixableItem in ItemsToFix.Where(x => x is IFileManipulationItem))
        {
            newFileContent = newFileContent.Replace(fixableItem.CurrentItemValue, fixableItem.ExpectedItemValue);
        }

        File.WriteAllText(FileName, newFileContent);
    }

    public string CurrentItemDisplayValue => FileName;

    // ReSharper disable once UnassignedGetOnlyAutoProperty - Unused in this class.
    public string ExpectedItemValue { get; }
    public string ExpectedItemDisplayValue => $"Items to fix: {ItemsToFix.Count}";
    public string CurrentItemValue { get; set; }
}