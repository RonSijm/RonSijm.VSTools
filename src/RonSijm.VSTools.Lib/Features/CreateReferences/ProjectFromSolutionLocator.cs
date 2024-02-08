namespace RonSijm.VSTools.Lib.Features.CreateReferences;

public class ProjectFromSolutionLocator
{
    public List<ProjectReferenceModel> GetProjectReferences(List<ProjectLoadedModel> loadedProjects)
    {
        var result = new List<ProjectReferenceModel>();

        foreach (var project in loadedProjects)
        {
            var projectHasProjectReferenceId = false;

            var itemGroups = project.Project.Children.Where(x => x is ProjectPropertyGroupElement).Cast<ProjectPropertyGroupElement>().ToList();
            foreach (var itemGroup in itemGroups)
            {
                foreach (var itemGroupChild in itemGroup.Children.Where(x => x is ProjectPropertyElement).Cast<ProjectPropertyElement>())
                {
                    if (itemGroupChild.Name == PropertySettings.ProjectReferenceIdName)
                    {
                        projectHasProjectReferenceId = true;

                        result.Add(new ProjectReferenceModel
                        {
                            Existing = true,
                            ProjectReferenceId = itemGroupChild.Value,
                            Project = project.Project,
                            File = project.File
                        });
                    }
                }

                if (projectHasProjectReferenceId)
                {
                    break;
                }
            }

            if (projectHasProjectReferenceId)
            {
                continue;
            }

            result.Add(new ProjectReferenceModel
            {
                Existing = false,
                ProjectReferenceId = Guid.NewGuid().ToString(),
                Project = project.Project
            });
        }

        return result;
    }
}