using System.Xml.Linq;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Resharper;

public static class ResharperDotProjectSettingsLoader
{
    public static IEnumerable<string> GetNamespaceFoldersToSkip(string projectPath)
    {
        var expectedSettingsPath = $"{projectPath}.DotSettings";
        var hasSettingsFile = File.Exists(expectedSettingsPath);

        if (!hasSettingsFile)
        {
            return [];
        }

        var settings = GetNamespaceFoldersToSkipFromSettings(expectedSettingsPath);

        return settings;
    }

    private static IEnumerable<string> GetNamespaceFoldersToSkipFromSettings(string settingsPath)
    {
        var xml = XDocument.Load(settingsPath);
        var itemsInXml = GetXmlItems(xml);

        var namespacesToSkip = XmlItemsToNamespacesToSkip(itemsInXml);

        return namespacesToSkip;
    }

    private static IEnumerable<string> XmlItemsToNamespacesToSkip(Dictionary<string, bool> itemsInXml)
    {
        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator - Justification : Kind of unreadable to debug
        foreach (var xmlItem in itemsInXml)
        {
            if (!xmlItem.Value)
            {
                continue;
            }

            if (!xmlItem.Key.StartsWith("/Default/CodeInspection/NamespaceProvider/NamespaceFoldersToSkip/=", StringComparison.Ordinal))
            {
                continue;
            }

            var namespaceToSkip = xmlItem.Key.Replace("/Default/CodeInspection/NamespaceProvider/NamespaceFoldersToSkip/=", string.Empty);
            namespaceToSkip = namespaceToSkip.Replace("/@EntryIndexedValue", string.Empty);
            namespaceToSkip = namespaceToSkip.Replace("_005C", ".");

            yield return $".{namespaceToSkip}";
        }
    }

    private static Dictionary<string, bool> GetXmlItems(XDocument xmlDocument)
    {
        var itemsInXml = new Dictionary<string, bool>();

        XNamespace booleanNamespace = "clr-namespace:System;assembly=mscorlib";
        XNamespace winfxNamespace = "http://schemas.microsoft.com/winfx/2006/xaml";
        var children = xmlDocument.Descendants(booleanNamespace + "Boolean").ToList();

        foreach (var booleanElement in children)
        {
            var key = booleanElement.Attribute(winfxNamespace + "Key")?.Value;
            if (key == null)
            {
                continue;
            }

            var value = bool.Parse(booleanElement.Value);

            itemsInXml.Add(key, value);
        }

        return itemsInXml;
    }
}