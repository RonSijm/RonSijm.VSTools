namespace RonSijm.VSTools.Lib.Features.MismatchFinding.NamespaceFixing.Core;

public class NamespaceSyntaxFixingFacade(ProjectReferenceLoader projectReferenceLoader, NamespaceValidator namespaceValidator, RoslynSyntaxFixingFacade roslynSyntaxFixingFacade) : IMismatchLocator
{
    public ushort Order => 4;
    public INamedCollection GetMismatches(CoreOptionsRequest options)
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
        return result;
    }
}