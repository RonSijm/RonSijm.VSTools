namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Helpers;

public static class NamespaceHelper
{
    public static string RemoveInvalidNamespaceCharacters(this string input)
    {
        return input?.Replace("-", "_");
    }

    public static List<string> GenerateRelativePaths(string callingNamespace, string calledNamespace)
    {
        var callingParts = callingNamespace.Split('.');
        var calledParts = calledNamespace.Split('.');

        var len = callingParts.Length > calledParts.Length ?
            calledParts.Length :
            callingParts.Length;

        var common = 0;
        while (common < len && callingParts[callingParts.Length - common - 1] == calledParts[calledParts.Length - common - 1])
        {
            common++;
        }

        var callsList = new List<string>();
        for (var i = common; i <= calledParts.Length; i++)
        {
            var call = string.Join(".", calledParts, calledParts.Length - i, i);
            callsList.Add(call);
        }

        return callsList.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }
}