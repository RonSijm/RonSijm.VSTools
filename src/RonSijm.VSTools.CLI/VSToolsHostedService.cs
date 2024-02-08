using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace RonSijm.VSTools.CLI;

public class VSToolsHostedService(ParsedCLIOptionsModel options, ILogger<VSToolsHostedService> logger, OptionsLogger optionsLogger, VSToolsLibService vstoolsLibService, AfterRunOptionsHelper afterRunOptionsHelper) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var runOptions = new Action<ParsedCLIOptionsModel>(_ => { });

        do
        {
            logger.CleanConsole();

            runOptions(options);

            // If you're running silently, we don't have to loop, and close the application after finishing
            runOptions = options.Silent ? null : runOptions;

            optionsLogger.LogActions(options);

            var result = vstoolsLibService.Fix(options);

            if (options.UpdateConfig && options.OptionsFile != null)
            {
                options.SaveOptions(options.OptionsFile);
            }

            if (runOptions != null)
            {
                runOptions = afterRunOptionsHelper.PrintInputOptions(result, options);
                Console.WriteLine();
            }
        } while (runOptions != null);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}