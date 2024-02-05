namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Models;

public static class ProjectWithFilesLoadedCollectionModelExtensions
{
    public static ProjectWithFilesLoadedCollectionModel ToModel(this IEnumerable<ProjectWithFilesLoadedModel> values)
    {
        return new ProjectWithFilesLoadedCollectionModel { Values = values.ToList() };
    }
}