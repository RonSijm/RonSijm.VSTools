using RonSijm.VSTools.Lib.Features.Core;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;

public class CollectionToFixResponse : List<SolutionToFixModel>, IFixable
{
    public void Fix()
    {
        foreach (var solutionToFixModel in this)
        {
            var solutionAsText = File.ReadAllText(solutionToFixModel.File);

            foreach (var project in solutionToFixModel.ItemsToFix)
            {
                solutionAsText = solutionAsText.Replace($"\"{project.Reference.Path}\"", $"\"{project.ExpectedPath}\"");
            }

            File.WriteAllText(solutionToFixModel.File, solutionAsText);
        }
    }
}