namespace RonSijm.VSTools.Lib.Features.Core.Extensions;

public static class HaveInnerReturnablesCountExtension
{
    public static int GetCount(this IHaveInnerReturnables item)
    {

        var relevantInnerItems = item.InnerItems.Where(x => x is IHaveInnerReturnables).Cast<IHaveInnerReturnables>().Sum(x => x.RelevantItemCount);
        var relevantItems = item.InnerItems.Count(x => x is not IHaveInnerReturnables);

        return relevantInnerItems + relevantItems;
    }
}