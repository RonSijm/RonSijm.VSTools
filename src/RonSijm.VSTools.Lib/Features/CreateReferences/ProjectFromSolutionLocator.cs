namespace RonSijm.VSTools.Lib.Features.CreateReferences;

public class ProjectFromSolutionLocator
{
    public List<ProjectReferenceModel> GetProjectReferences(List<ProjectRootElement> loadedProjects)
    {
        var result = new List<ProjectReferenceModel>();

        foreach (var project in loadedProjects)
        {
            var projectHasProjectReferenceId = false;

            var itemGroups = project.Children.Where(x => x is ProjectPropertyGroupElement).Cast<ProjectPropertyGroupElement>().ToList();
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
                            Project = project
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
                Project = project
            });
        }

        return result;
    }
}