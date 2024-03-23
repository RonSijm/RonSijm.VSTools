namespace RonSijm.VSTools.Core.DataContracts.FileModels;

public partial class ItemReference
{
    public static implicit operator ItemReference(ProjectInSolution projectInSolution)
    {
        return new ItemReference
        {
            Path = projectInSolution.RelativePath,
        };
    }
}