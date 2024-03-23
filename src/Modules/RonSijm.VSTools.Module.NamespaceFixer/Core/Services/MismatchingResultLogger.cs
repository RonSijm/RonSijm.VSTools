using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;
using RonSijm.VSTools.Module.NamespaceFixer.Core.Helpers;

namespace RonSijm.VSTools.Module.NamespaceFixer.Core.Services;

public class MismatchingResultLogger(IAsyncLogger<MismatchingResultLogger> logger)
{
    public async Task LogResults(INamedCollection results, int level = 0)
    {
        var itemCountToFix = results.RelevantItemCount;
        var hasRelevantItems = itemCountToFix != 0;

        if (hasRelevantItems)
        {
            await logger.LogInformationIndented(level, "{itemType} to Fix [{itemCount}]: {ObjectName}", results.ObjectType, itemCountToFix, results.ObjectName ?? string.Empty);
        }

        if (results is ICanHaveErrors resultWithErrors)
        {
            await LogErrors(results, resultWithErrors, level + 1);
        }

        if (hasRelevantItems)
        {
            await LogItems(results, level + 1);
        }
    }

    private async Task LogItems(IHaveInnerReturnables results, int level)
    {
        foreach (var result in results.InnerItems)
        {
            if (result is ILoggableItem loggableItem)
            {
                var typeName = result is IHaveObjectType objectType ? objectType.ObjectType : string.Empty;

                await logger.LogInformationIndented(level, "{typeName} Is:        {AbsoluteCurrentItemPath}", typeName, loggableItem.CurrentItemDisplayValue);
                await logger.LogInformationIndented(level, "{typeName} Should Be: {AbsoluteExpectedItemPath}", typeName, loggableItem.ExpectedItemDisplayValue);
            }

            if (result is INamedCollection itemsToFixCollection)
            {
                await LogResults(itemsToFixCollection, level);
            }
        }
    }

    private async Task LogErrors(INamedCollection results, ICanHaveErrors resultWithErrors, int level)
    {
        if (resultWithErrors.Errors.Count == 0)
        {
            return;
        }

        await logger.LogWarningIndented(level, "[{itemType}] {ObjectName}  has errors:", results.ObjectType, results.ObjectName);

        foreach (var errorToFixModel in resultWithErrors.Errors)
        {
            await logger.LogInformationIndented(level, "Error: {Error}", errorToFixModel.Error);

            if (errorToFixModel.ErrorReferences == null)
            {
                continue;
            }

            foreach (var errorReference in errorToFixModel.ErrorReferences)
            {
                await logger.LogErrorIndented(level, "{Error}", errorReference);
            }
        }
    }
}