namespace RonSijm.VSTools.Lib.Features.MismatchFinding.Core;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface ICoreFunction
{
    VSToolResult Run(CoreOptionsRequest options);

    public ModeEnum Function { get; }
}