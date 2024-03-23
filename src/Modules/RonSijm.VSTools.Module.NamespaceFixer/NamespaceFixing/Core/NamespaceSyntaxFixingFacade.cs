using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.NamespaceModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.FileValidation;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation;
using RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Services;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.Core;

public class NamespaceSyntaxFixingFacade(ProjectReferenceLoader projectReferenceLoader, NamespaceValidator namespaceValidator, RoslynSyntaxFixingFacade roslynSyntaxFixingFacade) : IMismatchLocator
{
    public ushort Order => 4;
    public Task<INamedCollection> GetMismatches(IFolderFixOptionsWithReferences options)
    {
        var projectReferences = projectReferenceLoader.GetProjectReferences(options);
        var loadedProjects = projectReferences.Select(projectMetadata => new ProjectWithFilesLoadedModel
        {
            ProjectRoot = ProjectRootElement.Open(projectMetadata.FileName),
            OtherNames = projectMetadata.OtherNames,
        }).ToModel();

        var allRenamedNamespaces = namespaceValidator.CheckNamespaces(loadedProjects);

        roslynSyntaxFixingFacade.FixSyntax(loadedProjects, allRenamedNamespaces);

        var itemsToFix = loadedProjects.SelectMany(x => x.Files).Where(x => x.InnerItems.Count != 0).ToList();

        var result = new SyntaxesToFixCollection();

        result.AddRange(itemsToFix);

        return Task.FromResult<INamedCollection>(result);
    }
}