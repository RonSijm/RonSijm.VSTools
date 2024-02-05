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

        FileLocator.FindFiles("*.cs", project.ProjectRoot.DirectoryPath, project.Files);

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
        
        var resharperNamespaceValidator = ResharperDotProjectSettingsLoader.GetNamespaceFoldersToSkip(project.ProjectRoot.FullPath).ToList();
        var excludedNamespace = NamespaceExclusionHelper.RemoveExcludes(namespaceExtension, resharperNamespaceValidator);

        var expectedNamespace = projectName + excludedNamespace;
        expectedNamespace = expectedNamespace.ToNamespace();

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
        yield return (currentRootNamespace, projectName);

        var classNames = fileModel.Root.DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();

        if (classNames.Any())
        {
            var namespaceVariants = NamespaceVariantGenerator.GenerateVariants(currentRootNamespace).ToList();

            foreach (var classDeclarationSyntax in classNames)
            {
                var className = classDeclarationSyntax.Identifier.ToString();

                foreach (var namespaceVariant in namespaceVariants)
                {
                    yield return ($"{namespaceVariant}.{className}", $"{projectName}.{className}");
                }
            }
        }
    }
}