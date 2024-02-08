namespace RonSijm.VSTools.CLI.Options.Core;

public static class OptionsModelConverter
{
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