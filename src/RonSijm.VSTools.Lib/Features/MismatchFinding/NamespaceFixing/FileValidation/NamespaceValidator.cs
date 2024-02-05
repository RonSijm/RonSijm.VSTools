using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Helpers;
using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation.Resharper;

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

        allRenamedNamespaces = allRenamedNamespaces.Rebuild();


        return allRenamedNamespaces;
    }

    private NamespaceChangedCollectionModel CheckNamespacesInProject(string projectFileName, ProjectWithFilesLoadedModel project)
    {
        var results = new NamespaceChangedCollectionModel();

        var projectName = projectFileName.Replace(".csproj", string.Empty);

        FileLocator.FindFilesWithoutBinFolder("*.cs", project.ProjectRoot.DirectoryPath, project.Files);
        FileLocator.FindFilesWithoutBinFolder("*.razor", project.ProjectRoot.DirectoryPath, project.Files);

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

                    results.Add(new NamespaceChangeModel(oldRootProjectNamespace + projectNamespace.Key, projectName + projectNamespace.Value, namespaceType));
                }
            }
        }

        foreach (var fileModel in project.Files)
        {
            BreakOnFileHelper.BreakOnFile(fileModel.FileName);

            var result = CheckNamespaceInFile(project, fileModel, projectName).ToList();
            results.AddRange(result);
        }

        return results;
    }

    private IEnumerable<NamespaceChangeModel> CheckNamespaceInFile(ProjectWithFilesLoadedModel project, FileToFixModel fileModel, string projectName)
    {
        var filePath = Path.GetDirectoryName(fileModel.FileName);

        if (filePath == null)
        {
            yield break;
        }

        var fileRelativePath = Path.GetRelativePath(project.ProjectRoot.FullPath, filePath);
        var namespaceExtension = fileRelativePath.Replace("..", string.Empty).Replace('\\', '.');

        var excludedNamespace = project.Namespaces[namespaceExtension];

        var expectedNamespace = $"{projectName}{excludedNamespace}";
        expectedNamespace = expectedNamespace.RemoveInvalidNamespaceCharacters();

        var fileContent = File.ReadAllText(fileModel.FileName);

        fileModel.SyntaxTree = CSharpSyntaxTree.ParseText(fileContent);
        fileModel.Root = fileModel.SyntaxTree.GetCompilationUnitRoot();
        fileModel.DescendantNodes = fileModel.Root.DescendantNodes().ToList();

        var fileNamespace = fileModel.DescendantNodes.GetNamespace();

        if (fileNamespace == null)
        {
            yield break;
        }

        var actualNamespace = fileNamespace.Name.ToString();

        if (actualNamespace == expectedNamespace)
        {
            yield break;
        }

        var itemToFix = new NamespaceDeclarationSyntaxFixer
        {
            ExpectedItemValue = expectedNamespace,
            CurrentItemValue = actualNamespace,
            ExpectedItemDisplayValue = expectedNamespace,
            CurrentItemDisplayValue = actualNamespace,
            Parent = fileModel
        };

        fileModel.ItemsToFix.Add(itemToFix);

        var currentRootNamespace = !string.IsNullOrWhiteSpace(namespaceExtension) ? actualNamespace.Replace(namespaceExtension, string.Empty) : actualNamespace;

        var classNames = fileModel.Root.DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();

        if (classNames.Any())
        {
            var namespaceVariants = NamespaceVariantGenerator.GenerateVariants(currentRootNamespace).ToList();

            foreach (var classDeclarationSyntax in classNames)
            {
                var className = classDeclarationSyntax.Identifier.ToString();

                foreach (var namespaceVariant in namespaceVariants)
                {
                    var namespaceType = currentRootNamespace == namespaceVariant ? NamespaceChangeType.Class : NamespaceChangeType.ClassVariant;
                    yield return new NamespaceChangeModel($"{namespaceVariant}.{className}", $"{projectName}.{className}", namespaceType);
                }
            }
        }
    }
}