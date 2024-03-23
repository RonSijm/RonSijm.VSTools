using System.ComponentModel;

using RonSijm.VSTools.Core.DataContracts;

namespace RonSijm.VSTools.Module.SolutionGenerator;

public class SolutionGeneratorOptions : IOptionsModel
{

    [Description("Other directories that might contain projects needed to be referenced. These projects themselves won't be fixed, but will just be used as references")]
    public List<string> ProjectReferences { get; set; }

    [Description("The project directories to inspect for Mismatching References")]
    public List<string> DirectoriesToInspect { get; set; }
}