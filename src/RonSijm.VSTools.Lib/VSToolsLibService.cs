using RonSijm.VSTools.Core.DataContracts;
using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.Lib;

public class VSToolsLibService(IAsyncLogger<VSToolsLibService> logger, IEnumerable<ICoreFunction> coreFunctions)
{
    public async Task<VSToolResult> Fix(CoreOptionsRequest options)
    {
        var requestedFunction = coreFunctions.FirstOrDefault(x => x.Function == options.Mode);

        if (requestedFunction != null)
        {
            return await requestedFunction.Run(options);
        }

        await logger.LogCritical("Argument out of range for mode {Mode}", options.Mode);
        return null;
    }
}