namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation;

public static class NamespaceVariantGenerator
{
    public static IEnumerable<string> GenerateVariants(string input)
    {
        var parts = input.Split('.');

        for (var i = parts.Length - 1; i >= 0; i--)
        {
            var variant = string.Join(".", parts, i, parts.Length - i);
            yield return variant;
        }
    }
}