using System.Reflection;
using RonSijm.VSTools.Lib.Features.Core;

namespace RonSijm.VSTools.CLI.DI;

public static class ServiceProviderFactory
{
    public static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
            .AddJsonFile("VSTools.appsettings.json")
            .Build();

        AddTypesAndInterfaces(services, typeof(VSToolFacade));
        AddTypesAndInterfaces(services, typeof(OptionsLogger));

        services.AddLogging(loggingBuilder =>
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            loggingBuilder.AddSerilog(logger, dispose: true);
        });

        services.AddTransient(typeof(Lazy<>), typeof(LazyProvider<>));

        var serviceProvider = services.BuildServiceProvider(false);
        return serviceProvider;
    }

    private static void AddTypesAndInterfaces(IServiceCollection services, Type targetType)
    {
        var types = targetType.Assembly.GetTypes().Where(x => !x.IsAbstract).ToList();

        foreach (var type in types)
        {
            services.AddTransient(type);

            var interfaces = type.GetInterfaces();

            foreach (var typeInterface in interfaces)
            {
                services.AddTransient(typeInterface, type);
            }
        }
    }
}