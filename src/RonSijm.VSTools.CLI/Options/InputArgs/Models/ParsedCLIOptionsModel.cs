namespace RonSijm.VSTools.CLI.Options.InputArgs.Models;

public class ParsedCLIOptionsModel : CLISaveOptionsModel
{
    // Silent in this file because it is a mode for the front-end, the Library does not break for user-input
    public bool Silent { get; set; }
    public bool UpdateConfig { get; set; }
    public string OptionsFile { get; set; }
}