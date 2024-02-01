﻿using RonSijm.VSTools.Lib.Features.CreateReferences.Models;

namespace RonSijm.VSTools.Lib.Features.Core.Options.Models;

public class CoreOptionsRequest
{
    /// <summary>
    /// The project directories to inspect for Mismatching References
    /// </summary>
    public List<string> DirectoriesToInspect { get; set; }

    /// <summary>
    /// Other directories that might contain projects needed to be referenced. These projects themselves won't be fixed, but will just be used as references
    /// </summary>
    public List<string> ProjectReferences { get; set; }

    /// <summary>
    /// Indicates whether you want to do a cold run, and only see the results.
    /// Instead of actually commiting the changes.
    /// </summary>
    public bool DoRealRun { get; set; }

    /// <summary>
    /// Indicate the mode in which to run.
    /// <see cref="ModeEnum"/>
    /// </summary>
    public ModeEnum Mode { get; set; } = ModeEnum.FindMismatches;

    /// <summary>
    /// A list of project references by their Ids, as created by the 
    /// </summary>
    public List<ProjectReferenceByIdModel> ProjectReferencesById { get; set; }
}