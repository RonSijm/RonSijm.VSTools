using RonSijm.VSTools.Lib.Features.SolutionFixing.Models;

namespace RonSijm.VSTools.Lib.Features.SolutionFixing.Services
{
    public static class SolutionReferenceFixer
    {
        /// <summary>
        /// Actually fixes the Mismatching References.
        /// </summary>
        public static void FixMismatchingReferences(this List<SolutionToFixModel> solutionsToFix)
        {
            foreach (var solutionToFixModel in solutionsToFix)
            {
                var solutionAsText = File.ReadAllText(solutionToFixModel.SolutionFile);

                foreach (var (project, expectedPath) in solutionToFixModel.ProjectsToFix)
                {
                    solutionAsText = solutionAsText.Replace($"\"{project.RelativePath}\"", $"\"{expectedPath}\"");
                }

                File.WriteAllText(solutionToFixModel.SolutionFile, solutionAsText);
            }
        }
    }
}
