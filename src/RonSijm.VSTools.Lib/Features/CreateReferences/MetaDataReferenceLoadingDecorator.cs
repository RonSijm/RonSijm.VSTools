namespace RonSijm.VSTools.Lib.Features.CreateReferences;

// ReSharper disable once UnusedType.Global - Used by DI
public class MetaDataReferenceLoadingDecorator : IReferenceLoadingDecorator
{
    public void LoadReferences(CoreOptionsRequest options, ProjectFileContainer allProjectReferences)
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