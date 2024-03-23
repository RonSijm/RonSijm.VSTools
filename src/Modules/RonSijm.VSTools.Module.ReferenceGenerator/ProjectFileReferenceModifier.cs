using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.ProjectFixingModels;

namespace RonSijm.VSTools.Module.ReferenceGenerator;

public class ProjectFileReferenceModifier
{
    public void AddReferenceToProjects(IEnumerable<ProjectReferenceModel> projectReferences)
    {
        foreach (var projectReferenceCacheModel in projectReferences.Where(projectReferenceCacheModel => !projectReferenceCacheModel.Existing))
        {
            AddReferenceToProject(projectReferenceCacheModel);
        }
    }

    public void AddReferenceToProject(ProjectReferenceModel projectReferenceModel)
    {
        var itemGroup = projectReferenceModel.Project.Children.FirstOrDefault(x => x is ProjectPropertyGroupElement) as ProjectPropertyGroupElement ?? projectReferenceModel.Project.AddPropertyGroup();

        itemGroup.AddProperty(PropertySettings.ProjectReferenceIdName, projectReferenceModel.ProjectReferenceId);
        projectReferenceModel.Project.Save();
    }
}