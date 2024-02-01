using RonSijm.VSTools.Lib.Features.Core.Options.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Models;
using RonSijm.VSTools.Lib.Features.MismatchFinding.SolutionFixing.Models;

namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

public interface IMismatchLocator
{
    /// <summary>
    /// Gets the Mismatching References and returns them.
    /// This method should not actually fix them, for the purpose of allowing you to do a cold-run, and display what would be fixed before commiting to actually fixing it.
    /// </summary>
    OneOf<ItemsToFixResponse, CollectionToFixResponse> GetMismatches(CoreOptionsRequest options);
}