using RonSijm.VSTools.Lib.Features.Core.Options.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

namespace RonSijm.VSTools.Lib.Features.CreateReferences.Interfaces;

public interface IReferenceLoadingDecorator
{
    void LoadReferences(CoreOptionsRequest options, ProjectFileContainer allProjectReferences);
}