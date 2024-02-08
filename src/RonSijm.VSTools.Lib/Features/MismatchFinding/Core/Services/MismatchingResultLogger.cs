using RonSijm.VSTools.Lib.Features.MismatchFinding.Core.Helpers;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.Core.Services;

public class MismatchingResultLogger(ILogger<MismatchingResultLogger> logger)
{
    public void LogResults(INamedCollection results, int level = 0)
    {
        var itemCountToFix = results.RelevantItemCount;
        var hasRelevantItems = itemCountToFix != 0;

        if (hasRelevantItems)
        {
            logger.LogInformationIndented(level, "{itemType} to Fix [{itemCount}]: {ObjectName}", results.ObjectType, itemCountToFix, results.ObjectName ?? string.Empty);
        }

        if (results is ICanHaveErrors resultWithErrors)
        {
            LogErrors(results, resultWithErrors, level + 1);
        }

        if (hasRelevantItems)
        {
            LogItems(results, level + 1);
        }
    }

    private void LogItems(IHaveInnerReturnables results, int level)
    {
        foreach (var result in results.InnerItems)
        {
            if (result is ILoggableItem loggableItem)
            {
                var typeName = result is IHaveObjectType objectType ? objectType.ObjectType : string.Empty;

                logger.LogInformationIndented(level, "{typeName} Is:        {AbsoluteCurrentItemPath}", typeName, loggableItem.CurrentItemDisplayValue);
                logger.LogInformationIndented(level, "{typeName} Should Be: {AbsoluteExpectedItemPath}", typeName, loggableItem.ExpectedItemDisplayValue);
            }

            if (result is INamedCollection itemsToFixCollection)
            {
                LogResults(itemsToFixCollection, level);
            }
        }
    }

    private void LogErrors(INamedCollection results, ICanHaveErrors resultWithErrors, int level)
    {
        if (resultWithErrors.Errors.Count == 0)
        {
            return;
        }

        logger.LogWarningIndented(level, "[{itemType}] {ObjectName}  has errors:", results.ObjectType, results.ObjectName);

        foreach (var errorToFixModel in resultWithErrors.Errors)
        {
            logger.LogInformationIndented(level, "Error: {Error}", errorToFixModel.Error);

            if (errorToFixModel.ErrorReferences == null)
            {
                continue;
            }

            foreach (var errorReference in errorToFixModel.ErrorReferences)
            {
                logger.LogErrorIndented(level, "{Error}", errorReference);
            }
        }
    }
}