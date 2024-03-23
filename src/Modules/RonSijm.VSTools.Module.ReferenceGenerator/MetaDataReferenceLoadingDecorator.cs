using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;
using RonSijm.VSTools.Core.DataContracts.References;

namespace RonSijm.VSTools.Module.ReferenceGenerator;

// ReSharper disable once UnusedType.Global - Used by DI
public class MetaDataReferenceLoadingDecorator : IReferenceLoadingDecorator
{
    public void LoadReferences(IOptionsWithReferences options, ProjectFileContainer allProjectReferences)
    {
        foreach (var allProjectReference in allProjectReferences)
        {
            var projectRootElement = ProjectRootElement.Open(allProjectReference.FileName);

            if (projectRootElement == null)
            {
                continue;
            }

            var itemGroups = projectRootElement.Children.Where(x => x is ProjectPropertyGroupElement).Cast<ProjectPropertyGroupElement>().ToList();

            var itemGroupChild = itemGroups.SelectMany(x => x.Children).Where(x => x is ProjectPropertyElement).Cast<ProjectPropertyElement>().FirstOrDefault(x => x.Name == PropertySettings.ProjectReferenceIdName);

            if (itemGroupChild == null)
            {
                continue;
            }

            var referenced = options.ProjectReferencesById?.FirstOrDefault(x => x.ProjectReferenceId == itemGroupChild.Value);

            if (referenced != null)
            {
                allProjectReference.OtherNames = referenced.ProjectNames;
            }
        }
    }
}