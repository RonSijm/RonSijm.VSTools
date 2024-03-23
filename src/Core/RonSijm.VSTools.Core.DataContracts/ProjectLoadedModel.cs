using RonSijm.VSTools.Core.DataContracts.FileModels;

namespace RonSijm.VSTools.Core.DataContracts;

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