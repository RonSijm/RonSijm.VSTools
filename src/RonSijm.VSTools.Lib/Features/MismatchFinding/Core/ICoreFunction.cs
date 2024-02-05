namespace RonSijm.VSTools.Lib.Features.MismatchFinding.Core;

public interface ICoreFunction
{
    VSToolResult Run(CoreOptionsRequest options);

    public ModeEnum Function { get; }
}