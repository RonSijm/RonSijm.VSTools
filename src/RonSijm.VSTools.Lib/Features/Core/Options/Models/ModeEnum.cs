namespace RonSijm.VSTools.Lib.Features.Core.Options.Models;

public enum ModeEnum
{
    /// <summary>
    /// A mode to actually run a scan for all the missing references.
    /// - When in ColdRun, it will show an overview of what's wrong.
    /// - When in Not In ColdRun, it will fix everything that's wrong.
    /// </summary>
    FindMismatches = 1,

    /// <summary>
    /// Creates a configuration for the current projects, and adds a ReferenceId to the projects.
    /// </summary>
    CreateReferences = 2,

    /// <summary>
    /// Creates a solution for all found projects
    /// </summary>
    CreateSolution = 3,
}