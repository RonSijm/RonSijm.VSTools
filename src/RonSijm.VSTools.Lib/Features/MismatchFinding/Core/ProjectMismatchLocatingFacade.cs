namespace RonSijm.VSTools.Lib.Features.MismatchFinding.Core;

public class ProjectMismatchLocatingFacade(IEnumerable<IMismatchLocator> mismatchLocators, MismatchingResultLogger mismatchingResultLogger) : ICoreFunction
{
    private readonly IEnumerable<IMismatchLocator> _mismatchLocators = mismatchLocators.OrderBy(x => x.Order).ToList();

    public ModeEnum Function => ModeEnum.FindMismatches;

    public VSToolResult Run(CoreOptionsRequest options)
    {
        var result = new VSToolResult();
        var interfaceResult = (IHaveInnerReturnablesOfT<INamedCollection>)result;

        foreach (var mismatchLocator in _mismatchLocators)
        {
            var projectResult = mismatchLocator.GetMismatches(options);
            mismatchingResultLogger.LogResults(projectResult);

            interfaceResult.Add(projectResult);

            if (!options.RealRun)
            {
                continue;
            }

            if (projectResult is IFixable fixable)
            {
                fixable.Fix();
            }
        }

        return result;
    }
}