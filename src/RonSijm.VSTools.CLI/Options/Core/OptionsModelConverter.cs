namespace RonSijm.VSTools.CLI.Options.Core;

public static class OptionsModelConverter
{
    public static CoreOptionsRequest Convert(this ParsedCLIOptionsModel model, CoreOptionsRequest result = null)
    {
        result ??= new CoreOptionsRequest();

        result.DirectoriesToInspect = model.DirectoriesToInspect;
        result.Mode = model.Mode;
        result.FolderFixMode = model.FolderFixMode;
        result.RealRun = model.RealRun;
        result.ProjectReferences = model.ProjectReferences;
        result.ProjectReferencesById = model.ProjectReferencesById;

        return result;
    }

    public static ParsedCLIOptionsModel Convert(this CLIOptionsModel value, ParsedCLIOptionsModel result = null)
    {
        result ??= new ParsedCLIOptionsModel();

        result.OptionsFile = value.OptionsFile ?? result.OptionsFile;
        result.Mode = value.Mode ?? result.Mode;
        result.FolderFixMode = value.FolderFixMode ?? result.FolderFixMode;
        result.RealRun = value.Run ?? result.RealRun;
        result.Silent = value.Silent ?? result.Silent;
        result.UpdateConfig = value.UpdateConfig ?? result.UpdateConfig;

        return result;
    }
}