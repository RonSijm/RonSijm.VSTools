using RonSijm.VSTools.Lib.Features.Core.Options.Models;

namespace RonSijm.VSTools.CLI.Options.Core;

public static class OptionsModelConverter
{
    public static CoreOptionsRequest Convert(this ParsedCLIOptionsModel model, CoreOptionsRequest result = null)
    {
        result ??= new CoreOptionsRequest();

        result.DirectoriesToInspect = model.DirectoriesToInspect;
        result.Mode = model.Mode;
        result.DoRealRun = model.Run;
        result.ProjectReferences = model.ProjectReferences;
        result.ProjectReferencesById = model.ProjectReferencesById;

        return result;
    }

    public static ParsedCLIOptionsModel Convert(this CLIOptionsModel value, ParsedCLIOptionsModel result = null)
    {
        result ??= new ParsedCLIOptionsModel();

        result.OptionsFile = value.OptionsFile;
        result.Mode = value.Mode ?? result.Mode;
        result.Run = value.Run ?? result.Run;
        result.Silent = value.Silent ?? result.Silent;
        result.UpdateConfig = value.UpdateConfig ?? result.UpdateConfig;

        return result;
    }
}