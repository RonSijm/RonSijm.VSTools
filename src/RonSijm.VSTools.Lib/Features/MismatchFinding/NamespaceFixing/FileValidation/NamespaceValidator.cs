namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation;

public class NamespaceValidator
{
    public NamespaceChangedCollectionModel CheckNamespaces(ProjectWithFilesLoadedCollectionModel loadedProjects)
    {
        var allRenamedNamespaces = new NamespaceChangedCollectionModel();

        foreach (var projectItem in loadedProjects)
        {
            var projectFilename = Path.GetFileName(projectItem.ProjectRoot.FullPath);

            if (projectFilename == null)
            {
                continue;
            }

            BreakOnFileHelper.BreakOnFile(projectFilename);

            var renamedNamespaces = CheckNamespacesInProject(projectFilename, projectItem);
            allRenamedNamespaces.AddRange(renamedNamespaces);
        }

        allRenamedNamespaces.Rebuild();

        return allRenamedNamespaces;
    }

    private NamespaceChangedCollectionModel CheckNamespacesInProject(string projectFileName, ProjectWithFilesLoadedModel project)
    {
        var results = new NamespaceChangedCollectionModel();

        var projectName = projectFileName.Replace(".csproj", string.Empty);

        var csFiles = FileLocator.FindFilesWithoutBinFolder("*.cs", project.ProjectRoot.DirectoryPath);

        foreach (var fileModel in csFiles)
        {
            project.Files.Add(new SyntaxInFileToFixModel { FileModel = fileModel });
        }

        var razorFiles = FileLocator.FindFilesWithoutBinFolder("*.razor", project.ProjectRoot.DirectoryPath);

        foreach (var fileModel in razorFiles)
        {
            project.Files.Add(new SyntaxInFileToFixModel { FileModel = fileModel });
        }

        var resharperNamespaceValidator = ResharperDotProjectSettingsLoader.GetNamespaceFoldersToSkip(project.ProjectRoot.FullPath).ToList();
        var directories = DirectoryLocator.GetAllDirectoriesWithoutBuildFolders(project.ProjectRoot.DirectoryPath);

        foreach (var directory in directories)
        {
            var nameSpace = directory.Replace('\\', '.');
            var excludedNamespace = NamespaceExclusionHelper.RemoveExcludes(nameSpace, resharperNamespaceValidator);
            project.Namespaces.Add(nameSpace, excludedNamespace);
        }

        if (project.OtherNames != null)
        {
            foreach (var otherProjectNames in project.OtherNames)
            {
                var oldRootProjectNamespace = otherProjectNames.Replace(".csproj", string.Empty);

                foreach (var projectNamespace in project.Namespaces)
                {
                    var namespaceType = string.IsNullOrWhiteSpace(projectNamespace.Key) ? NamespaceChangeType.Project : NamespaceChangeType.Folder;

                    results.Add(new NamespaceReferenceModel(oldRootProjectNamespace + projectNamespace.Key, projectName + projectNamespace.Value, namespaceType));
                }
            }
        }

        foreach (var fileModel in project.Files)
        {
            BreakOnFileHelper.BreakOnFile(fileModel.FileModel.FileName);

            var result = CheckNamespaceInFile(project, fileModel, projectName).ToList();
            results.AddRange(result);
        }

        return results;
    }

    private IEnumerable<NamespaceReferenceModel> CheckNamespaceInFile(ProjectWithFilesLoadedModel project, SyntaxInFileToFixModel fileModel, string projectName)
    {
        var filePath = Path.GetDirectoryName(fileModel.FileModel.FileName);

        if (filePath == null)
        {
            yield break;
        }

        var fileRelativePath = Path.GetRelativePath(project.ProjectRoot.FullPath, filePath);
        var namespaceExtension = fileRelativePath.Replace("..", string.Empty).Replace('\\', '.');

        var excludedNamespace = project.Namespaces[namespaceExtension];

        var expectedNamespace = $"{projectName}{excludedNamespace}";
        expectedNamespace = expectedNamespace.RemoveInvalidNamespaceCharacters();

        var fileContent = File.ReadAllText(fileModel.FileModel.FileName);

        fileModel.SyntaxTree = CSharpSyntaxTree.ParseText(fileContent);
        fileModel.Root = fileModel.SyntaxTree.GetCompilationUnitRoot();
        fileModel.DescendantNodes = fileModel.Root.DescendantNodes().ToList();
        fileModel.Namespace = expectedNamespace;

        var fileNamespace = fileModel.DescendantNodes.GetNamespace();

        if (fileNamespace == null)
        {
            yield break;
        }

        var actualNamespace = fileNamespace.Name.ToString();

        if (actualNamespace != expectedNamespace)
        {
            var itemToFix = new NamespaceDeclarationSyntaxFixer
            {
                ExpectedItemValue = expectedNamespace,
                CurrentItemValue = actualNamespace,
                ExpectedItemDisplayValue = expectedNamespace,
                CurrentItemDisplayValue = actualNamespace,
                Parent = fileModel
            };

            fileModel.Add(itemToFix);
        }


        var currentRootNamespace = !string.IsNullOrWhiteSpace(namespaceExtension) ? actualNamespace.Replace(namespaceExtension, string.Empty) : actualNamespace;

        var itemIdentifiers = new List<string>();

        var typeDeclarationSyntaxes = fileModel.Root.DescendantNodes().OfType<BaseTypeDeclarationSyntax>().ToList();
        var declarationSyntaxes = fileModel.Root.DescendantNodes().OfType<DelegateDeclarationSyntax>().ToList();

        itemIdentifiers.AddRange(typeDeclarationSyntaxes.Select(x => GetIdentifierName(x)));
        itemIdentifiers.AddRange(declarationSyntaxes.Select(x => x.Identifier.ToString()));
        itemIdentifiers.AddRange(declarationSyntaxes.Select(GetIdentifierName));

        if (itemIdentifiers.Count == 0)
        {
            yield break;
        }

        var namespaceVariants = NamespaceVariantGenerator.GenerateVariants(actualNamespace).ToList();

        foreach (var namespaceVariant in namespaceVariants)
        {
            foreach (var typeDeclarationName in itemIdentifiers)
            {
                var namespaceType = currentRootNamespace == namespaceVariant ? NamespaceChangeType.Class : NamespaceChangeType.ClassVariant;

                if (namespaceVariant == string.Empty)
                {
                    yield return new NamespaceReferenceModel($"{typeDeclarationName}", $"{projectName}{excludedNamespace}.{typeDeclarationName}", namespaceType);
                }
                else
                {
                    yield return new NamespaceReferenceModel($"{namespaceVariant}.{typeDeclarationName}", $"{projectName}{excludedNamespace}.{typeDeclarationName}", namespaceType);
                }
            }

            var namespaceTypeNamespace = currentRootNamespace == namespaceVariant ? NamespaceChangeType.Class : NamespaceChangeType.ClassVariant;

            if (namespaceVariant != string.Empty)
            {
                yield return new NamespaceReferenceModel($"{namespaceVariant}", $"{projectName}{excludedNamespace}", namespaceTypeNamespace);
            }

        }

        var projectNamespaceVariants = NamespaceVariantGenerator.GenerateVariants(currentRootNamespace).ToList();


        foreach (var namespaceVariant in namespaceVariants)
        {
            foreach (var typeDeclarationName in itemIdentifiers)
            {
                if (namespaceVariant != string.Empty)
                {
                    yield return new NamespaceReferenceModel($"{namespaceVariant}", $"{projectName}", NamespaceChangeType.Project);
                }
            }
        }
    }

    private string GetIdentifierName(DelegateDeclarationSyntax declarationSyntax)
    {
        if (declarationSyntax.Parent is BaseTypeDeclarationSyntax parent)
        {
            return GetIdentifierName(parent, "." + declarationSyntax.Identifier);
        }

        return declarationSyntax.Identifier.ToString();
    }

    public static string GetIdentifierName(BaseTypeDeclarationSyntax baseTypeDeclarationSyntax, string postfix = "")
    {
        if (baseTypeDeclarationSyntax.Parent is BaseTypeDeclarationSyntax parent)
        {
            return GetIdentifierName(parent, $".{baseTypeDeclarationSyntax.Identifier}{postfix}");
        }

        var result = baseTypeDeclarationSyntax.Identifier + postfix;

        return result;
    }
}