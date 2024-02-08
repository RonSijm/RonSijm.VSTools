namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;

[DebuggerDisplay("{ProjectRoot.FullPath}")]
public class ProjectWithFilesLoadedModel
{
    public ProjectRootElement ProjectRoot { get; set; }
    public List<SyntaxInFileToFixModel> Files { get; set; } = [];
    public Dictionary<string, string> Namespaces { get; set; } = [];
    public List<string> OtherNames { get; set; }

    public void Deconstruct(out ProjectRootElement projectRoot, out List<SyntaxInFileToFixModel> files)
    {
        projectRoot = ProjectRoot;
        files = Files;
    }
}