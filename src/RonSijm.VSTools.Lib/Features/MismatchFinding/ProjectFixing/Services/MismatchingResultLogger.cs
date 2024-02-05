namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public class MismatchingResultLogger(ILogger<MismatchingResultLogger> logger)
{
    public void LogProjectResults(OneOf<ItemsToFixResponse, CollectionToFixResponse> results)
    {
        if (results.IsT0)
        {
            LogSingleResult(results.AsT0);
        }
        else
        {
            LogMultipleResults(results.AsT1);
        }
    }

    private void LogMultipleResults(CollectionToFixResponse results)
    {
        if (results.Count != 0)
        {
            logger.LogInformation("Solutions to Fix:");
        }

        foreach (var solutionToFixModel in results)
        {
            logger.LogInformation("Solution: {SolutionFile}", solutionToFixModel.File);
            LogSingleResult(solutionToFixModel, false);
        }
    }

    private void LogSingleResult(ItemsToFixResponse results, bool mentionWhichProject = true)
    {
        if (results.ItemsToFix.Count != 0)
        {
            logger.LogInformation("Projects to Fix:");
        }

        foreach (var result in results.ItemsToFix)
        {
            if (mentionWhichProject)
            {
                logger.LogInformation("Project: {AbsoluteProjectPath}", result.CurrentItemDisplayValue);
            }
            
            logger.LogInformation("Has Reference:       {AbsoluteCurrentItemPath}", result.CurrentItemValue);
            logger.LogInformation("Should Be Reference: {AbsoluteExpectedItemPath}", result.ExpectedItemDisplayValue);
        }

        if (results.Errors.Count != 0)
        {
            logger.LogWarning("Project has errors:");
        }

        foreach (var errorToFixModel in results.Errors)
        {
            logger.LogInformation("Error: {Error}", errorToFixModel.Error);

            if (errorToFixModel.ErrorReferences != null)
            {
                foreach (var errorReference in errorToFixModel.ErrorReferences)
                {
                    logger.LogError("{Error}", errorReference);
                }
            }
        }
    }
}