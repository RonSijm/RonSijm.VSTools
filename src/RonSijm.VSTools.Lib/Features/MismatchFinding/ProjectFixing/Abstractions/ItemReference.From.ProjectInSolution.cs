namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Abstractions;

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