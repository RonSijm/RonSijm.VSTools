namespace RonSijm.VSTools.Lib.Features.Core;

public class VSToolsLibService(ILogger<VSToolsLibService> logger, IEnumerable<ICoreFunction> coreFunctions)
{
    public VSToolResult Fix(CoreOptionsRequest options)
    {
        var requestedFunction = coreFunctions.FirstOrDefault(x => x.Function == options.Mode);

        if (requestedFunction != null)
        {
            return requestedFunction.Run(options);
        }

        logger.LogCritical("Argument out of range for mode {Mode}", options.Mode);
        return null;
    }
}