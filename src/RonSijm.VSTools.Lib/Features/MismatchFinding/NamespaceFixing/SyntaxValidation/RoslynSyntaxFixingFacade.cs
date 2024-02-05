using System.Text.RegularExpressions;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation;

public class RoslynSyntaxFixingFacade
{
    public void FixSyntax(ProjectWithFilesLoadedCollectionModel projects, NamespaceChangedCollectionModel allRenamedNamespaces)
    {
        foreach (var (projectRootElement, fileList) in projects)
        {
            foreach (var fileModel in fileList)
            {
                BreakOnFileHelper.BreakOnFile(fileModel.FileName);

                FixFile(fileModel, allRenamedNamespaces);
            }
        }
    }

    public void FixFile(FileToFixModel fileModel, NamespaceChangedCollectionModel allRenamedNamespaces)
    {
        FixUsingDirectives(fileModel, allRenamedNamespaces);
        FixMemberAccessExpression(fileModel, allRenamedNamespaces);
        FixMemberReturnExpression(fileModel, allRenamedNamespaces);
        FixAttributes(fileModel, allRenamedNamespaces);

        if (fileModel.FileName.EndsWith(".razor", StringComparison.InvariantCultureIgnoreCase))
        {
            FixRazorUsings(fileModel, allRenamedNamespaces);
        }
    }

    private void FixRazorUsings(FileToFixModel fileModel, NamespaceChangedCollectionModel fileNamespaces)
    {
        var text = File.ReadAllText(fileModel.FileName);
        var usingMatches = Regex.Matches(text, @"@using\s+(.*)");

        foreach (Match usingMatch in usingMatches)
        {
            var currentItemValue = usingMatch.Value.Replace("\r", string.Empty).Replace("\n", string.Empty);
            var actualNamespace = currentItemValue.Split(' ').Last();

            var isRenamed = GetNamespace(fileNamespaces, actualNamespace);

            if (isRenamed == null)
            {
                continue;
            }

            var expectedNamespace = actualNamespace.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

            if (actualNamespace == expectedNamespace)
            {
                continue;
            }

            var replaceNamespace = currentItemValue.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

            var itemToFix = new StringReplaceFixable
            {
                ExpectedItemValue = replaceNamespace,
                CurrentItemValue = currentItemValue,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
            };

            fileModel.ItemsToFix.Add(itemToFix);
        }
    }

    private void FixMemberAccessExpression(FileToFixModel fileModel, NamespaceChangedCollectionModel fileNamespaces)
    {
        var memberAccessExpressionSyntaxes = fileModel.Root.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();

        foreach (var memberAccessExpressionSyntax in memberAccessExpressionSyntaxes)
        {
            var actualNamespace = memberAccessExpressionSyntax.ToString();

            var isRenamed = GetNamespace(fileNamespaces, actualNamespace);

            if (isRenamed == null)
            {
                continue;
            }

            var expectedNamespace = actualNamespace.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

            if (actualNamespace == expectedNamespace)
            {
                continue;
            }

            var itemToFix = new MemberAccessExpressionSyntaxFixer
            {
                ExpectedItemValue = expectedNamespace,
                CurrentItemValue = actualNamespace,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
                Parent = fileModel,
            };

            fileModel.ItemsToFix.Add(itemToFix);
        }
    }

    public void FixMemberReturnExpression(FileToFixModel fileModel, NamespaceChangedCollectionModel fileNamespaces)
    {
        var ddd = fileModel.Root.DescendantNodes().ToList();
        var nodes = fileModel.Root.DescendantNodes().ToList();
        var qualifiedNameSyntaxToFix = fileModel.Root.DescendantNodes().OfType<QualifiedNameSyntax>().ToList();

        foreach (var qualifiedNameSyntax in qualifiedNameSyntaxToFix)
        {
            var actualNamespace = qualifiedNameSyntax.ToString();

            var isRenamed = GetNamespace(fileNamespaces, actualNamespace);

            if (isRenamed == null)
            {
                continue;
            }

            var expectedNamespace = actualNamespace.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

            if (actualNamespace == expectedNamespace)
            {
                continue;
            }

            var itemToFix = new QualifiedNameSyntaxFixer
            {
                ExpectedItemValue = expectedNamespace,
                CurrentItemValue = actualNamespace,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
                Parent = fileModel,
            };

            fileModel.ItemsToFix.Add(itemToFix);
        }
    }

    public static void FixUsingDirectives(FileToFixModel fileModel, NamespaceChangedCollectionModel fileNamespaces)
    {
        var usingDeclarations = fileModel.DescendantNodes.OfType<UsingDirectiveSyntax>().ToList();

        foreach (var namespaceDeclaration in usingDeclarations)
        {
            var actualNamespace = namespaceDeclaration.NormalizeWhitespace().NamespaceOrType.ToString();

            var isRenamed = GetNamespace(fileNamespaces, actualNamespace);

            if (isRenamed == null)
            {
                continue;
            }

            var expectedNamespace = actualNamespace.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

            if (actualNamespace == expectedNamespace)
            {
                continue;
            }

            var itemToFix = new UsingDirectiveRenameFixer
            {
                ExpectedItemValue = expectedNamespace,
                CurrentItemValue = actualNamespace,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
                Parent = fileModel,
            };

            fileModel.ItemsToFix.Add(itemToFix);
        }
    }

    private static NamespaceChangeModel GetNamespace(NamespaceChangedCollectionModel fileNamespaces, string actualNamespace)
    {
        var isRenamed = fileNamespaces.FirstOrDefault(x => actualNamespace.Equals(x.OldNamespace, StringComparison.OrdinalIgnoreCase));

        if (isRenamed == null)
        {
            isRenamed = fileNamespaces.FirstOrDefault(x => actualNamespace.StartsWith(x.OldNamespace, StringComparison.OrdinalIgnoreCase));

            if (isRenamed != null)
            {
                if (isRenamed.OldNamespace == isRenamed.ExpectedNamespace)
                {
                    return null;
                }

            }
        }

        return isRenamed;
    }

    private static void FixAttributes(FileToFixModel fileModel, NamespaceChangedCollectionModel fileNamespaces)
    {
        var literalExpressions = fileModel.DescendantNodes.OfType<AttributeArgumentSyntax>().ToList();

        foreach (var literalExpressionSyntax in literalExpressions)
        {
            var literalValueQuoted = literalExpressionSyntax.ToString();

            if (!literalValueQuoted.StartsWith('"'))
            {
                continue;
            }
            
            var literalValueUnquoted = literalValueQuoted.Substring(1, literalValueQuoted.Length - 2);

            var isRenamed = GetNamespace(fileNamespaces, literalValueUnquoted);

            if (isRenamed == null)
            {
                continue;
            }

            var expectedNamespace = literalValueUnquoted.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

            if (literalValueUnquoted == expectedNamespace)
            {
                continue;
            }

            var itemToFix = new AttributeArgumentSyntaxFixer
            {
                ExpectedItemValue = $"\"{expectedNamespace}\"",
                CurrentItemValue = literalValueQuoted,
                ExpectedItemDisplayValue = $"\"{expectedNamespace}\"",
                CurrentItemDisplayValue = literalValueQuoted,
                Parent = fileModel,
            };

            fileModel.ItemsToFix.Add(itemToFix);
        }
    }
}