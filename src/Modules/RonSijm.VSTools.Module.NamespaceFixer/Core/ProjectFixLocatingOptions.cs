using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.CoreModels;

namespace RonSijm.VSTools.Module.NamespaceFixer.Core;

public class ProjectFixLocatingOptions : IFolderFixOptionsWithReferences, IOptionsModel, IHavePreview
{
    public RunModeEnum RealRun { get; set; }
    public List<string> DirectoriesToInspect { get; set; }
    public FolderFixModeEnum FolderFixMode { get; set; }

    public List<string> ProjectReferences { get; set; }

    public List<ProjectReferenceByIdModel> ProjectReferencesById { get; set; }
}