using CommandLine;

using RonSijm.VSTools.Core.DataContracts.CoreModels;

namespace RonSijm.VSTools.CLI.Options.InputArgs.Models;

public class CLICommandModel
{
    [Option('i', nameof(IgnoreSettingsFile), Required = false, HelpText = "Only uses command line parameters, ignores a settings file when one is found in the working directory.")]
    public bool? IgnoreSettingsFile { get; set; }

    [Option('s', nameof(Silent), Required = false, HelpText = "Runs in Silent Mode - Doesn't ask for confirmation prompts, and ends with finished.")]
    public bool? Silent { get; set; }

    [Option('u', nameof(UpdateConfig), Required = false, HelpText = "Option to save the config.\n\rIf using an existing config file, the file is updated.\n\rIf using a working directory, the file is saved there.")]
    public bool? UpdateConfig { get; set; }

    [Option('r', nameof(Run), Required = false, HelpText = "Indicates that you want to do a real-run instead of a cold run.\n\rThat only shows the results, but doesn't actually updates anything yet.")]
    public RunModeEnum? Run { get; set; }

    [Option('m', nameof(Mode), Required = false, HelpText = @$"Indicate the mode in which to run:
 - '{nameof(ModeEnum.Interactive)}' - Lets you interact without doing anything by default.
 - '{nameof(ModeEnum.CreateReferences)}' - Creates a configuration for the current projects, and adds a ReferenceId to the projects. 
 - '{nameof(ModeEnum.FindMismatches)}' - A mode to actually run a scan for all the missing references.")]
    public ModeEnum? Mode { get; set; }

    [Option('f', nameof(FolderFixMode), Required = false, HelpText = @$"Indicate the mode in how to fix folders being different than their project name.
 - '{nameof(FolderFixModeEnum.DoNothing)}' - Indicates to leave things as-is. 
 - '{nameof(FolderFixModeEnum.RenameProject)}' - Indicates to rename the Project file to match the Folder name.
 - '{nameof(FolderFixModeEnum.RenameFolder)}' - Indicates to rename the Folder to match the Project file name.")]
    public FolderFixModeEnum? FolderFixMode { get; set; }

    [Option('o', nameof(OptionsFile), Required = false, HelpText = "Options file to use.")]
    public string OptionsFile { get; set; }
}