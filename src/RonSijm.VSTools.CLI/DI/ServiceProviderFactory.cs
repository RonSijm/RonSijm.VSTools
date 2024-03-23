using MassTransit;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using RonSijm.VSTools.CLI.Transit;
using RonSijm.VSTools.Core.DataContracts.ReturningDataContracts.SyntaxModels;
using RonSijm.VSTools.Core.Logging.Features.Dispatching;
using RonSijm.VSTools.Lib;
using RonSijm.VSTools.Module.NamespaceFixer.Core;
using RonSijm.VSTools.Module.NamespaceFixer.NamespaceFixing.SyntaxValidation.Helpers;
using RonSijm.VSTools.Module.ReferenceGenerator;
using RonSijm.VSTools.Module.SolutionGenerator;
using Terminal.Gui;

namespace RonSijm.VSTools.CLI.DI;

public static class ServiceProviderFactory
{
    public static IServiceCollection ForCLI(this IServiceCollection services)
    {
        AddTypesAndInterfaces(services, typeof(Program));

        return services;
    }

    public static IServiceCollection ForVSToolsLib(this IServiceCollection services)
    {
        AddTypesAndInterfaces(services, typeof(VSToolsLibService));
        AddTypesAndInterfaces(services, typeof(SolutionGeneratorFacade));
        AddTypesAndInterfaces(services, typeof(ProjectReferenceMappingFacade));
        AddTypesAndInterfaces(services, typeof(ProjectMismatchLocatingFacade));

        SyntaxInFileToFixModel.FixableComparer = new FixableItemComparer();

        return services;
    }

    public static IServiceCollection WithOptions(this IServiceCollection services, ParsedCLIOptionsModel options)
    {
        services.AddSingleton(options);

        return services;
    }
    
    public static IServiceCollection WithLogging(this IServiceCollection services, string logSettingsFile = null)
    {
        var configuration = logSettingsFile == null ? 
            new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
                .AddJsonFile("VSTools.appsettings.json").Build() : 
            
            new ConfigurationBuilder()
                .AddJsonFile(logSettingsFile)
                .Build();

        services.AddLogging(loggingBuilder =>
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            loggingBuilder.AddSerilog(logger, dispose: true);

            Log.Logger = logger;
        });

        return services;
    }

    public static IServiceCollection WithEvents(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            
            x.AddConsumers(entryAssembly);

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        // I don't know how to automatically register open generics though reflection...
        services.AddScoped(typeof(IAsyncLogger<>), typeof(AsyncLogger<>));

        services.Add(new ServiceDescriptor(typeof(IAsyncLogger<>), typeof(AsyncLogger<>), ServiceLifetime.Scoped));

        return services;
    }

    private static void AddTypesAndInterfaces(IServiceCollection services, Type targetType)
    {
        var types = targetType.Assembly.GetTypes().Where(x => !x.IsAbstract && !x.IsGenericType).ToList();

        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<Lifetime.ServiceLifetimeAttribute>();

            var lifetime = attribute?.ServiceLifetime ?? ServiceLifetime.Transient;

            var interfaces = type.GetInterfaces();

            if (interfaces.Length == 0 || type.IsAssignableTo(typeof(View)) || type.IsAssignableTo(typeof(ReactiveObject)))
            {
                services.Add(new ServiceDescriptor(type, type, lifetime));
            }
            else
            {
                foreach (var typeInterface in interfaces)
                {
                    // Skipping hosted Services to manually assign them, and start them in the correct order.
                    if (typeInterface.IsAssignableFrom(typeof(IHostedService)))
                    {
                        continue;
                    }

                    services.Add(new ServiceDescriptor(typeInterface, type, lifetime));
                }
            }
        }
    }
}