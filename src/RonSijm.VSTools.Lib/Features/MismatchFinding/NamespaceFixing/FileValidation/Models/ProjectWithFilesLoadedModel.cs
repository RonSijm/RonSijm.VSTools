namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;

public class ProjectWithFilesLoadedModel
{
    public ProjectRootElement ProjectRoot { get; set; }
    public List<FileToFixModel> Files { get; set; } = [];

    public void Deconstruct(out ProjectRootElement projectRoot, out List<FileToFixModel> files)
    {
        projectRoot = ProjectRoot;
        files = Files;
    }
}