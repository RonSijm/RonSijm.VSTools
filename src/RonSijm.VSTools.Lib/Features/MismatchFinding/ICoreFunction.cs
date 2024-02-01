using RonSijm.VSTools.Lib.Features.Core;
using RonSijm.VSTools.Lib.Features.Core.Options.Models;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding;

public interface ICoreFunction
{
    VSToolResult Run(CoreOptionsRequest options);

    public ModeEnum Function { get; }
}