namespace RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;

public class SolutionsToFixCollectionModel : List<SolutionToFixModel>, IFixable
{
    public void Fix()
    {
        foreach (var solutionToFixModel in this)
        {
            var solutionAsText = File.ReadAllText(solutionToFixModel.File);

            foreach (var project in solutionToFixModel.ItemsToFix)
            {
                solutionAsText = solutionAsText.Replace($"\"{project.CurrentItemValue}\"", $"\"{project.ExpectedItemValue}\"");
            }

            File.WriteAllText(solutionToFixModel.File, solutionAsText);
        }
    }
}