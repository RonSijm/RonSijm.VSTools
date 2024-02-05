namespace RonSijm.VSTools.Lib.Features.MismatchFinding.Core;

public class ProjectMismatchLocatingFacade(IEnumerable<IMismatchLocator> mismatchLocators, MismatchingResultLogger mismatchingResultLogger) : ICoreFunction
{
    private readonly IEnumerable<IMismatchLocator> _mismatchLocators = mismatchLocators.OrderBy(x => x.Order).ToList();

    public ModeEnum Function => ModeEnum.FindMismatches;

    public VSToolResult Run(CoreOptionsRequest options)
    {
        var result = new VSToolResult();

        foreach (var mismatchLocator in _mismatchLocators)
        {
            var projectResult = mismatchLocator.GetMismatches(options);
            mismatchingResultLogger.LogProjectResults(projectResult);
            result.Results.Add(projectResult);

            if (!options.DoRealRun)
            {
                continue;
            }

            switch (projectResult)
            {
                case { IsT0: true, AsT0: IFixable singleFixable }:
                    singleFixable.Fix();
                    break;
                case { IsT1: true, AsT1: IFixable collectionFixable }:
                    collectionFixable.Fix();
                    break;
            }
        }

        return result;
    }
}