namespace RonSijm.VSTools.Core.DataContracts.CoreInterfaces;

public interface IOptionsWithReferences
{
    public List<ProjectReferenceByIdModel> ProjectReferencesById { get; set; }
}