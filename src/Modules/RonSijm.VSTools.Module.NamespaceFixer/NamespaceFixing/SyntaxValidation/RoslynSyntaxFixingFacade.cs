using RonSijm.VSTools.Core.DataContracts.Helpers;
using RonSijm.VSTools.Core.DataContracts.NamespaceModels;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Interfaces;

namespace RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation;

public class RoslynSyntaxFixingFacade(IEnumerable<ISyntaxMismatchLocator> syntaxMismatchLocator)
{
    private readonly List<ISyntaxMismatchLocator> _syntaxMismatchLocator = syntaxMismatchLocator.OrderBy(x => x.Order).ToList();

    public void FixSyntax(ProjectWithFilesLoadedCollectionModel projects, NamespaceChangedCollectionModel namespaceCollection)
    {
        foreach (var (projectRootElement, fileList) in projects)
        {
            foreach (var fileModel in fileList)
            {
                BreakOnFileHelper.BreakOnFile(fileModel.FileModel.FileName);

                foreach (var syntaxMismatchLocator in _syntaxMismatchLocator.Where(syntaxMismatchLocator => syntaxMismatchLocator.CanHandle(fileModel)))
                {
                    syntaxMismatchLocator.Locate(fileModel, namespaceCollection);
                }
            }
        }
    }
}