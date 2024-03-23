using RonSijm.VSTools.Core.DataContracts.CoreModels;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts;

namespace RonSijm.VSTools.Core.DataContracts;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface ICoreFunction
{
    string FunctionDescription { get; }
    
    Task<VSToolResult> Run(object options);

    public ModeEnum Function { get; }
}

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface ICoreFunction<in T> : ICoreFunction where T : class, IOptionsModel
{
    Task<VSToolResult> ICoreFunction.Run(object options)
    {
        var typedOptions = options as T;
        return Run(typedOptions);
    }

    Task<VSToolResult> Run(T options);
}