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
    }

    private void FixMemberAccessExpression(FileToFixModel fileModel, NamespaceChangedCollectionModel fileNamespaces)
    {
        var memberAccessExpressionSyntaxes = fileModel.Root.DescendantNodes().OfType<MemberAccessExpressionSyntax>().ToList();

        foreach (var memberAccessExpressionSyntax in memberAccessExpressionSyntaxes)
        {
            var actualNamespace = memberAccessExpressionSyntax.ToString();

            var isRenamed = fileNamespaces.FirstOrDefault(x => x.OldNamespace.Equals(actualNamespace, StringComparison.OrdinalIgnoreCase));

            if (isRenamed == default)
            {
                continue;
            }

            var expectedNamespace = actualNamespace.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

            var itemToFix = new IdentifierNameSyntaxFixer
            {
                ExpectedItemValue = isRenamed.ExpectedNamespace,
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
        var qualifiedNameSyntaxToFix = fileModel.Root.DescendantNodes().OfType<QualifiedNameSyntax>().ToList();

        foreach (var qualifiedNameSyntax in qualifiedNameSyntaxToFix)
        {
            var actualNamespace = qualifiedNameSyntax.ToString();

            var isRenamed = fileNamespaces.FirstOrDefault(x => x.OldNamespace.Equals(actualNamespace, StringComparison.OrdinalIgnoreCase));

            if (isRenamed == default)
            {
                continue;
            }

            var expectedNamespace = actualNamespace.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

            var itemToFix = new QualifiedNameSyntaxFixer
            {
                ExpectedItemValue = isRenamed.ExpectedNamespace,
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

            var isRenamed = fileNamespaces.FirstOrDefault(x => actualNamespace.StartsWith(x.OldNamespace, StringComparison.OrdinalIgnoreCase));

            if (isRenamed == default)
            {
                continue;
            }

            var expectedNamespace = actualNamespace.Replace(isRenamed.OldNamespace, isRenamed.ExpectedNamespace);

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

            var isRenamed = fileNamespaces.FirstOrDefault(x => x.OldNamespace == literalValueQuoted);

            if (isRenamed == default)
            {
                continue;
            }

            var itemToFix = new AttributeArgumentSyntaxFixer
            {
                ExpectedItemValue = $"\"{isRenamed.ExpectedNamespace}\"",
                CurrentItemValue = literalValueQuoted,
                ExpectedItemDisplayValue = $"\"{isRenamed.ExpectedNamespace}\"",
                CurrentItemDisplayValue = literalValueQuoted,
                Parent = fileModel,
            };

            fileModel.ItemsToFix.Add(itemToFix);
        }
    }
}