namespace RonSijm.VSTools.CLI.Options.InputArgs.Models;

/// <summary>
/// This is a different model than <see cref="ParsedCLIOptionsModel"/> because we do want to save these properties in the output,
/// But the <see cref="CoreOptionsRequest"/> does not need these options, so we won't send them to the library.
/// </summary>
public class CLISaveOptionsModel : CoreOptionsRequest
{
    public string LoggingOptionsFile { get; set; }
}