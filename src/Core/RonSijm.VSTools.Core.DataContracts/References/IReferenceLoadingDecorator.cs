using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;

namespace RonSijm.VSTools.Core.DataContracts.References;

public interface IReferenceLoadingDecorator
{
    void LoadReferences(IOptionsWithReferences options, ProjectFileContainer allProjectReferences);
}