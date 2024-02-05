namespace RonSijm.VSTools.Lib.Features.CreateReferences.Interfaces;

public interface IReferenceLoadingDecorator
{
    void LoadReferences(CoreOptionsRequest options, ProjectFileContainer allProjectReferences);
}