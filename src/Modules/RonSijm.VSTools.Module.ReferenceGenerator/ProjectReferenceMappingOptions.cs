using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.CoreModels;

namespace RonSijm.VSTools.Module.ReferenceGenerator;

public class ProjectReferenceMappingOptions : IOptionsModel, IHavePreview
{
    public List<string> DirectoriesToInspect { get; set; }
    public RunModeEnum RealRun { get; set; }
}