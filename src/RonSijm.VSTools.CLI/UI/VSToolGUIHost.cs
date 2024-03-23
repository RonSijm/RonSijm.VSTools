using System.Globalization;
using System.Reactive.Concurrency;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using RonSijm.VSTools.CLI.UI.Helpers;
using RonSijm.VSTools.CLI.UI.Main;
using RonSijm.VSTools.CLI.UI.Services;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;
using Terminal.Gui;

namespace RonSijm.VSTools.CLI.UI;

public class VSToolGUIHost(MainWindowView mainWindowView, IAsyncLogger<VSToolGUIHost> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Application.UseSystemConsole = true;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Application.Init();
        Colors.Base = ColorSchemeProvider.ColorScheme;

        RxApp.MainThreadScheduler = TerminalScheduler.Default;
        RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;

        Application.Top.Add(mainWindowView);

        await logger.LogInformation("Application started");

        DoLoggingStuff(cancellationToken);

        Application.Run(Application.Top);
    }

    private void DoLoggingStuff(CancellationToken cancellationToken)
    {
        // ReSharper disable once AsyncVoidLambda
        var task = new Task(async () =>
        {
            while (true)
            {
                await logger.LogInformation(DateTime.Now.ToString(CultureInfo.InvariantCulture));
                await Task.Delay(5000, cancellationToken);
            }
            // ReSharper disable once FunctionNeverReturns
        });

        task.Start();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}