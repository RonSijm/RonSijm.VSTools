namespace RonSijm.VSTools.Lib.Features.Core.Models;

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