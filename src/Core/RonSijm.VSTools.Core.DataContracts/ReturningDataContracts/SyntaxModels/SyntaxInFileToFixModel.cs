using RonSijm.VSTools.Core.DataContracts.FileModels;
using RonSijm.VSTools.Core.DataContracts.Helpers;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;

namespace RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;

public class SyntaxInFileToFixModel : FileFixableCollectionBase<IReturnable>
{
    public static IComparer<IReturnable> FixableComparer { get; set; }

    public SyntaxTree SyntaxTree { get; set; }
    public CompilationUnitSyntax Root { get; set; }
    public List<SyntaxNode> DescendantNodes { get; set; }

    public override string ObjectType => "Syntax In File";

    public override string ObjectName => FileModel.FileName;
    public string Namespace { get; set; }

    public override void Fix()
    {
        if (InnerItems.Count == 0)
        {
            return;
        }

        BreakOnFileHelper.BreakOnFile(FileModel.FileName);

        var syntaxFixing = InnerItems.Where(x => x is IFixable).Order(FixableComparer).Cast<IFixable>().ToList();

        foreach (var fixableItem in syntaxFixing)
        {
            fixableItem.Fix();
        }

        var newFileContent = FileExportFormatExtension.SyntaxNodeToString(this);

        foreach (var fixableItem in InnerItems.Where(x => x is IValueFix).Cast<IValueFix>())
        {
            newFileContent = newFileContent.Replace(fixableItem.CurrentItemValue, fixableItem.ExpectedItemValue);
        }

        File.WriteAllText(FileModel.FileName, newFileContent);

        Clear();
    }
}