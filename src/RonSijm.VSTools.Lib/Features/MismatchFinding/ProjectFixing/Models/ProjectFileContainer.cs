namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

public class ProjectFileContainer : List<ProjectFileModel>
{
    public List<string> FindProject(string fileName)
    {
        var referencePaths = this.Where(referencePath =>
        {
            var referenceFileName = Path.GetFileName(referencePath.File);

            if (referenceFileName == null)
            {
                return false;
            }

            var currentMatches = referenceFileName.Equals(fileName, StringComparison.InvariantCultureIgnoreCase);

            if (currentMatches)
            {
                return true;
            }

            var otherMatch = referencePath.OtherNames?.FirstOrDefault(x => x.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));

            if (otherMatch == null)
            {
                return false;
            }

            var newFileName = Path.GetFileName(referencePath.File);

            if (!referencePath.OtherNames.Any(x => x.Equals(newFileName)))
            {
                referencePath.OtherNames.Add(newFileName);
            }

            return true;
        }).ToList();

        return referencePaths.Select(x => x.File).ToList();
    }
}