using System.Threading.Tasks;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;

namespace RonSijm.VSTools.CLI.Options.Services;

public class OptionsParseService(IAsyncLogger<OptionsParseService> logger = null)
{
    public async Task<ParsedCLIOptionsModel> CreateOptions(CLICommandModel cliParseResult)
    {
        return await cliParseResult.CreateOptions(logger);
    }
}