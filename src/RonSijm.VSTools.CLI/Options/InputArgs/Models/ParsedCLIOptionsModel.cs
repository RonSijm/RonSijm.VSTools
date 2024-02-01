using RonSijm.VSTools.Lib.Features.Core.Options.Models;
using RonSijm.VSTools.Lib.Features.CreateReferences.Models;

namespace RonSijm.VSTools.CLI.Options.InputArgs.Models;

public class ParsedCLIOptionsModel
{
    public bool Silent { get; set; }
    public bool UpdateConfig { get; set; }

    public bool Run { get; set; }

    public ModeEnum Mode { get; set; } = ModeEnum.FindMismatches;

    public string OptionsFile { get; set; }

    /// <summary>
    /// The project directories to inspect for Mismatching References
    /// </summary>
    public List<string> DirectoriesToInspect { get; set; }

    /// <summary>
    /// Other directories that might contain projects needed to be referenced. These projects themselves won't be fixed, but will just be used as references
    /// </summary>
    public List<string> ProjectReferences { get; set; }

    /// <summary>
    /// A list of project references by their Ids, as created by the 
    /// </summary>
    public List<ProjectReferenceByIdModel> ProjectReferencesById { get; set; }
}