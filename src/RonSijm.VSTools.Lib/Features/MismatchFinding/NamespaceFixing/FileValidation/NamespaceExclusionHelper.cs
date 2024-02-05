namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation;

public class NamespaceExclusionHelper
{
    public static string RemoveExcludes(string input, List<string> excludeList)
    {
        var output = input;

        var collidingExclusions = ListCollidingExclusions(excludeList).ToList();
        var otherExclusions = excludeList.Where(x => !collidingExclusions.Contains(x)).ToList();

        foreach (var exclusion in collidingExclusions)
        {
            if (!output.StartsWith(exclusion, StringComparison.InvariantCultureIgnoreCase))
            {
                continue;
            }

            var split = exclusion.Split(".").Length;
            var currentInput = output.Split(".").ToList();

            var result = string.Join(".", currentInput.GetRange(0, split - 1));

            output = output.Replace(exclusion, result, StringComparison.InvariantCultureIgnoreCase);

            for (var index = 0; index < otherExclusions.Count; index++)
            {
                otherExclusions[index] = otherExclusions[index].Replace(exclusion, result, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        foreach (var exclusion in otherExclusions)
        {
            if (!output.StartsWith(exclusion, StringComparison.InvariantCultureIgnoreCase))
            {
                continue;
            }

            var split = exclusion.Split(".").ToList();
            var currentInput = output.Split(".").ToList();
            var result = string.Join(".", currentInput.GetRange(0, split.Count - 1));

            output = output.Replace(exclusion, result, StringComparison.InvariantCultureIgnoreCase);
        }


        return output;
    }

    private static IEnumerable<string> ListCollidingExclusions(List<string> excludeList)
    {
        foreach (var exclude in excludeList)
        {
            var startsWith = false;

            foreach (var excludeInner in excludeList.Where(excludeInner => excludeInner != exclude).Where(excludeInner => excludeInner.StartsWith(exclude, StringComparison.InvariantCultureIgnoreCase)))
            {
                startsWith = true;
            }

            if (startsWith)
            {
                yield return exclude;
            }
        }
    }
}