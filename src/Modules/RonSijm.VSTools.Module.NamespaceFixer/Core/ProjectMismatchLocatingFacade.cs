using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.ItemInterfaces;
using RonSijm.VSTools.Module.NamespaceFixer.Core.Services;
using RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Services;

namespace RonSijm.VSTools.Module.NamespaceFixer.Core;

public class ProjectMismatchLocatingFacade(IEnumerable<IMismatchLocator> mismatchLocators, MismatchingResultLogger mismatchingResultLogger) : ICoreFunction<ProjectFixLocatingOptions>
{
    private readonly IEnumerable<IMismatchLocator> _mismatchLocators = mismatchLocators.OrderBy(x => x.Order).ToList();

    public ModeEnum Function => ModeEnum.FindMismatches;

    public string FunctionDescription => "Finds projects and solutions that reference projects, which project references are incorrectly mapped.";

    public async Task<VSToolResult> Run(ProjectFixLocatingOptions options)
    {
        var result = new VSToolResult();
        var interfaceResult = (IHaveInnerReturnablesOfT<INamedCollection>)result;

        foreach (var mismatchLocator in _mismatchLocators)
        {
            var projectResult = await mismatchLocator.GetMismatches(options);
            await mismatchingResultLogger.LogResults(projectResult);

            interfaceResult.Add(projectResult);

            if (options.RealRun != RunModeEnum.Real)
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