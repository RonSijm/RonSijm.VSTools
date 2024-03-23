using System.Threading;
using System.Threading.Tasks;
using RonSijm.VSTools.CLI.Options.Services;
using RonSijm.VSTools.Lib;

namespace RonSijm.VSTools.CLI;

public class VSToolsHostedService(ParsedCLIOptionsModel options, OptionsLoggingService optionsLoggingService, VSToolsLibService vstoolsLibService, InteractiveOptionsHelper interactiveOptionsHelper)
{
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        var runOptions = new Action<ParsedCLIOptionsModel>(_ => { });


        do
        {
            runOptions(options);

            // If you're running silently, we don't have to loop, and close the application after finishing
            runOptions = options.Silent ? null : runOptions;

            await optionsLoggingService.LogActions(options);

            var result = await vstoolsLibService.Fix(options);

            if (options.UpdateConfig && options.OptionsFile != null)
            {
                options.SaveOptions(options.OptionsFile);
            }

            if (runOptions != null)
            {
                runOptions = await interactiveOptionsHelper.PrintInputOptions(result, options);
                Console.WriteLine();
            }
        } while (runOptions != null);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}