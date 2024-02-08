namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Helpers;

public static class NamespaceHelper
{
    public static string RemoveInvalidNamespaceCharacters(this string input)
    {
        return input?.Replace("-", "_");
    }

    public static string FindCommonRootNamespace(string namespace1, string namespace2)
    {
        var parts1 = namespace1.Split('.');
        var parts2 = namespace2.Split('.');

        var commonRootIndex = 0;
        while (commonRootIndex < Math.Min(parts1.Length, parts2.Length) && parts1[commonRootIndex] == parts2[commonRootIndex])
        {
            commonRootIndex++;
        }

        return string.Join(".", parts1, 0, commonRootIndex);
    }

    public static string FindNonCommonRootNamespace(string namespace1, string namespace2)
    {
        var parts1 = namespace1.Split('.');
        var parts2 = namespace2.Split('.');

        var commonRootIndex = 0;

        while (commonRootIndex < Math.Min(parts1.Length, parts2.Length) && parts1[commonRootIndex] == parts2[commonRootIndex])
        {
            commonRootIndex++;
        }

        return string.Join(".", parts2, commonRootIndex, parts2.Length - commonRootIndex);
    }
}