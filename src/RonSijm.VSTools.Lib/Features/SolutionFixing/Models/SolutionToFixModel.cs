using Microsoft.Build.Construction;

namespace RonSijm.VSTools.Lib.Features.SolutionFixing.Models
{
    public class SolutionToFixModel(string solutionFile)
    {
        public string SolutionFile { get; } = solutionFile;

        public List<(ProjectInSolution reference, string expectedPath)> ProjectsToFix { get; set; } = new();
        public List<(ProjectInSolution reference, string error)> Errors { get; set; } = new();
    }
}