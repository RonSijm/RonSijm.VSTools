using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.Hosting;
using RonSijm.VSTools.CLI.Options.Services;
using RonSijm.VSTools.CLI.UI;

namespace RonSijm.VSTools.CLI;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        // Try to double-click action for our file extension
        ProgramSettings.ProgramFileExtension.CreateFileExtensionAssociation("VSTools", "Tool for Visual Studio Solution and Project based fixes.", $"--{nameof(CLICommandModel.OptionsFile)} \"%1\"");

        var parsedSettings = Parser.Default.ParseArguments<CLICommandModel>(args);

        // Preload the settings before running the application, so we know whether you want to do any logging.
        // And so we can inject the settings into the DI Container.
        var settings = await parsedSettings.Value.CreateOptions();
        
        // Build DI Container, and start the app.
        var host = Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
        {
            services.ForCLI()
                .ForVSToolsLib()
                .WithLogging(settings.LoggingOptionsFile)
                .WithOptions(settings)
                .WithEvents();

            services.AddHostedService<VSToolGUIHost>();

        }).UseSerilog();

        await host.Build().RunAsync();

        return 1;
    }
}