using RonSijm.VSTools.Lib.Features.CreateReferences.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;

namespace RonSijm.VSTools.Lib.Features.CreateReferences;

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