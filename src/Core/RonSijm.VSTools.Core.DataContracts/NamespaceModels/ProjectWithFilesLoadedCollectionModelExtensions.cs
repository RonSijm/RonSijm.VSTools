namespace RonSijm.VSTools.Core.DataContracts.NamespaceModels;

public static class ProjectWithFilesLoadedCollectionModelExtensions
{
    public static ProjectWithFilesLoadedCollectionModel ToModel(this IEnumerable<ProjectWithFilesLoadedModel> values)
    {
        return new ProjectWithFilesLoadedCollectionModel { Values = values.ToList() };
    }
}