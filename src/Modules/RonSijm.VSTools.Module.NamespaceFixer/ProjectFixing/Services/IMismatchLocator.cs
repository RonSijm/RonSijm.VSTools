using RonSijm.VSTools.Core.DataContracts.CoreInterfaces;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.CollectionInterfaces;

namespace RonSijm.VSTools.Module.NamespaceFixer.ProjectFixing.Services;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)]
public interface IMismatchLocator
{
    /// <summary>
    /// Gets the order of fixers to run
    /// </summary>
    ushort Order { get; }

    /// <summary>
    /// Gets the Mismatching References and returns them.
    /// This method should not actually fix them, for the purpose of allowing you to do a cold-run, and display what would be fixed before commiting to actually fixing it.
    /// </summary>
    Task<INamedCollection> GetMismatches(IFolderFixOptionsWithReferences options);
}