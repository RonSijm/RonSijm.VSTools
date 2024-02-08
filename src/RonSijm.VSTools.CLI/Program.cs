using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace RonSijm.VSTools.CLI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var baseServiceProvider = ServiceProviderFactory.CreateServices().WithLogging().BuildServiceProvider();
        var optionsFromArgsParser = baseServiceProvider.GetRequiredService<OptionsFromArgsParser>();
        var options = optionsFromArgsParser.Load(args);

        if (options == null)
        {
            Console.WriteLine("Input parameters were invalid.");
            return;
        }

        await Host.CreateDefaultBuilder(args).ConfigureServices((_, services) =>
        {
            ServiceProviderFactory.CreateServices(services).RegisterVSToolsLib().WithLogging(options.LoggingOptionsFile).WithOptions(options);
        }).UseSerilog()
            .RunConsoleAsync();
    }
}