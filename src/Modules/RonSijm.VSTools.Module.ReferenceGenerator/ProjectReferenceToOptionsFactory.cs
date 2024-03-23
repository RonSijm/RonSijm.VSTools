using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.CoreModels;

namespace RonSijm.VSTools.Module.ReferenceGenerator;

public class ProjectReferenceToOptionsFactory
{
    public void SaveReferencesToOptions(CoreOptionsRequest options, List<ProjectReferenceModel> projectReferences)
    {
        options.ProjectReferencesById ??= [];

        foreach (var projectReferenceCacheModel in projectReferences)
        {
            var fileName = Path.GetFileName(projectReferenceCacheModel.Project.FullPath);

            var existingReferenceItem = options.ProjectReferencesById.FirstOrDefault(x => x.ProjectReferenceId == projectReferenceCacheModel.ProjectReferenceId);

            if (existingReferenceItem == null)
            {
                options.ProjectReferencesById.Add(new ProjectReferenceByIdModel
                {
                    ProjectReferenceId = projectReferenceCacheModel.ProjectReferenceId,
                    ProjectNames = [fileName]
                });
            }
            else
            {
                if (existingReferenceItem.ProjectNames.All(x => x != fileName))
                {
                    existingReferenceItem.ProjectNames.Add(fileName);
                }
            }
        }
    }
}