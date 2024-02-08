namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ProjectLoadedModel
{
    public ProjectLoadedModel(ProjectRootElement project, FileModel file)
    {
        Project = project;
        File = file;
    }

    public ProjectLoadedModel()
    {
    }

    public ProjectRootElement Project { get; set;  }
    public FileModel File { get; set; }
}