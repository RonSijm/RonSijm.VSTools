using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.FileValidation;
using RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.SyntaxValidation;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.Core;

public class NamespaceSyntaxFixingFacade(ProjectReferenceLoader projectReferenceLoader, NamespaceValidator namespaceValidator, RoslynSyntaxFixingFacade roslynSyntaxFixingFacade) : IMismatchLocator
{
    public ushort Order => 4;
    public OneOf<ItemsToFixResponse, CollectionToFixResponse> GetMismatches(CoreOptionsRequest options)
    {
        var projectReferences = projectReferenceLoader.GetProjectReferences(options);
        var loadedProjects = projectReferences.Select(projectMetadata => new ProjectWithFilesLoadedModel { ProjectRoot = ProjectRootElement.Open(projectMetadata.FileName) }).ToModel();

        var allRenamedNamespaces = namespaceValidator.CheckNamespaces(loadedProjects);

        roslynSyntaxFixingFacade.FixSyntax(loadedProjects, allRenamedNamespaces);

        var itemsToFix = loadedProjects.SelectMany(x => x.Files).Where(x => x.ItemsToFix.Count != 0).ToList();

        var result = new ItemsToFixResponse();

        result.AddRange(itemsToFix);
        return result;
    }
}