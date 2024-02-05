namespace RonSijm.VSTools.Lib.Features.MismatchFinding.ProjectFixing.Services;

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
    OneOf<ItemsToFixResponse, SolutionsToFixCollectionModel> GetMismatches(CoreOptionsRequest options);
}