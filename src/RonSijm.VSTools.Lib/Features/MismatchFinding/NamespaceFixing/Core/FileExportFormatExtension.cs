namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.Core;

public static class FileExportFormatExtension
{
    public static string SyntaxNodeToString(FileToFixModel file)
    {
        var newFileContent = file.FileName.EndsWith(".razor", StringComparison.OrdinalIgnoreCase) ? file.Root.ToFullString() : 
            file.Root.NormalizeWhitespace(elasticTrivia: true).ToFullString();

        return newFileContent;
    }
}