using RonSijm.VSTools.Lib.Features.Core;
using RonSijm.VSTools.Lib.Features.Core.Options.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding;

public class ProjectMismatchLocatingFacade(IEnumerable<IMismatchLocator> mismatchLocators, MismatchingResultLogger mismatchingResultLogger) : ICoreFunction
{
    public ModeEnum Function => ModeEnum.FindMismatches;

    public VSToolResult Run(CoreOptionsRequest options)
    {
        var result = new VSToolResult();

        foreach (var mismatchLocator in mismatchLocators)
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